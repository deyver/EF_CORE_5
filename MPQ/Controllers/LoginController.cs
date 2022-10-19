using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPQ.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using Microsoft.Extensions.Localization;
using MPQ.Utils.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using MPQ.Utils;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using MPQ.Models.WebServices;
using MPQ.Utils.I18n;
using System.Runtime.Versioning;
using MPQ.Helpers;
using MPQ.Utils.Permissao;
using MPQ.Data.Repositories;
using MPQ.Domain;

namespace MPQ.Controllers
{
    public class LoginController : MasterController
    {
        private readonly IHttpContextAccessor _context;
        private readonly ICultureFactory _cultureFactory;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUsuarioAplicacao _usuAplicacaoHelper;
        private readonly IPermissaoHelper _permissaoHelper;
        private readonly IAutenticacaoHelper _autenticacaoHelper;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;

        public LoginController(
            IConfiguration configuration,
            IHttpContextAccessor context,
            ICultureFactory cultureFactory,
            IPermissaoHelper permissaoHelper,
            IUsuarioAplicacao usuAplicacaoHelper,
            IAutenticacaoHelper autenticacaoHelper,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUserRepository userRepository,
            ISiteRepository siteRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._context = context;
            this._cultureFactory = cultureFactory;
            this._permissaoHelper = permissaoHelper;
            this._usuAplicacaoHelper = usuAplicacaoHelper;
            this._autenticacaoHelper = autenticacaoHelper;
            this._sharedLocalizer = sharedLocalizer;
            this._userRepository = userRepository;
            this._siteRepository = siteRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            if (ConsultaTodosDominio().Count() == 0)
            {
                AddErrosModel("Erro ao obter a lista de Domínios. Por favor, contate o TI Central.");
                return View();
            }

            var cookie = Request.Cookies["mpq_login_session"];
            if (cookie == null)
            {
                // Redireciona para a tela de login.
                ViewBag.Dominio = ConsultaTodosDominio();
                return View();
            }

            // Verifica o valor do cookie decodificado
            var dadosCookie = StringUtils.Base64Decode(cookie ?? "");
            var dominio = dadosCookie.Split("\\")[0];
            var usuario = dadosCookie.Split("\\")[1];

            // Verifica se o usuário ainda está liberado no sistema.
            if (!ExisteUsuarioCadastrado(new LoginViewModel { Dominio = dominio, Login = usuario }).Result)
            {
                ExcluirCookie();
                ViewBag.Dominio = ConsultaTodosDominio();
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            ViewBag.Dominio = ConsultaTodosDominio();
            ViewBag.Erro = "";

            // Caso não estiver válido, já retorna o erro.
            if (!ModelState.IsValid) return View("Index", model);

            try
            {
                // Verifica se o login e senha estão corretos no AD.
                if (ActiveDirectoryAuthenticate(model.Login, model.Senha))
                {
                    // Verifica se o usuário está liberado no sistema.
                    if (!ExisteUsuarioCadastrado(model).Result)
                    {
                        // Retorna uma mensagem de erro, mostrando que o usuário não tem cadastro no sistema
                        AddErrosModel("Usuário não possui acesso ao sistema.");
                        return View("Index", model);
                    }

                    //Preenche objeto estatico do usuario
                    LoginViewModel login = ConsultaUsuarioPorLogin(model.Login, model.Dominio).Result;
                    model.DefaultSiteId = login.DefaultSiteId;

                    #region Configuração do Cookie
                    // 30 minutos para expirar o cookie

                    UsuarioViewModel user = new UsuarioViewModel() { Id = login.Id, Login = login.Login, Name = login.Name, DefaultSiteId = login.DefaultSiteId, DefaultLanguage = login.DefaultLanguage, Status = login.Status };
                    string valueCookie = StringUtils.Base64Encode("SA" + "\\" + login.Login + "\\" + login.DefaultSiteId + "|" + GetSite(login.DefaultSiteId) + "\\" + login.Name + "\\" + Environment.MachineName.ToString() + "\\" + login.DefaultLanguage + "\\" + login.Id);

                    this._autenticacaoHelper.GravaSessao(valor: valueCookie);
                    this._usuAplicacaoHelper.Armazenar(user);

                    //this._permissaoUsuarioService.LoadPermissoes(idUsuario: usuario.Id, idPlanta: usuario.IdPlantaPadrao, idIdioma: usuDados.IdIdiomaPadrao);

                    Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(login.DefaultLanguage)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

                    #endregion

                    return RedirectToAction("Index", "Home");
                }

                // Retorna uma mensagem de erro, mostrando que o usuário não está no AD.
                AddErrosModel("Erro ao realizar login, verifique o usuário e a senha.");
                return View("Index", model);

            }
            catch (Exception exc)
            {
                ViewBag.Erro = exc.Message;

                return View("Index", model);
            }
        }

        [Route("Login/Sair")]
        public IActionResult Logout()
        {
            if (Request.Cookies["mpq_login_session"] != null)
            {
                ExcluirCookie();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Método para verificar se um usuário está ativo no AD.
        /// </summary>
        /// <param name="usuario">Usuário</param>
        /// <param name="senha">Senha</param>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        private bool ActiveDirectoryAuthenticate(string usuario, string senha)
        {
            // TODO: Retirar
            #if DEBUG
            return true;
            #endif

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, this._configuration.GetSection("LDAP").GetSection("Instance").Value))
                {
                    return context.ValidateCredentials(usuario, senha);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void ExcluirCookie()
        {
            Response.Cookies.Delete("mpq_login_session");
        }

        /// <summary>
        /// Verifica se o usuário está cadastrado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<bool> ExisteUsuarioCadastrado(LoginViewModel model)
        {
            var user = await _userRepository.GetByLoginAsync(model.Login);
            return (user?.Login == model.Login && model.Login.Trim() != "");
        }

        /// <summary>
        /// Consulta Usuario completo para recuperar a PlantaPadrão
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="dominio"></param>
        /// <returns></returns>
        private async Task<LoginViewModel> ConsultaUsuarioPorLogin(string idUsuario, string dominio)
        {
            var user = await _userRepository.GetByLoginAsync(idUsuario);
            return new LoginViewModel { Dominio = dominio, Login = user.Login, Id = user.Id, Name = user.Name, DefaultSiteId = user.DefaultSiteId, DefaultLanguage = user.DefaultLanguage, Status = user.Status };
        }

        private List<SelectListItem> ConsultaTodosDominio()
        {
            try
            {
                //Requisitar no banco de Dados
                var lista = new List<SelectListItem>();
                lista.Add(new SelectListItem() { Selected = true, Value = "SA", Text = "SA", });
                return lista;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        private string GetSite(int siteId)
        {
            return (siteId == 0 ? "" : _siteRepository.GetByIdAsync(siteId).Result.Name);
        }

    }
}
