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
using Microsoft.AspNetCore.Authentication;
using Novell.Directory.Ldap;
using Microsoft.Extensions.Configuration;
using MPQ.Helpers;
using MPQ.Models.WebServices;
using MPQ.Data.Repositories;
using MPQ.Domain;
using Newtonsoft.Json;
using MPQ.Utils;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace MPQ.Controllers
{
    public class HomeController : MasterController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IAutenticacaoHelper _autenticacaoHelper;

        public HomeController(
            ILogger<HomeController> logger, 
            IConfiguration configuration,
            IUserRepository userRepository,
            ISiteRepository siteRepository,
            IAutenticacaoHelper autenticacaoHelper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._logger = logger;
            this._userRepository = userRepository;
            this._siteRepository = siteRepository;
            this._autenticacaoHelper = autenticacaoHelper;
        }

        [Route("Home")]
        public IActionResult Index(string msg = "")
        {
            var cookie = Request.Cookies["mpq_login_session"];
            if (cookie == null)
            {
                // Redireciona para a tela de login.
                return RedirectToAction("Index", "Login");
            }

            if (!string.IsNullOrEmpty(msg))
            {
                if (msg == "401")
                {
                    ViewData["alerta"] = "Seu usuário não possui acesso a este recurso.";
                }
                else
                {
                    ViewData["alerta"] = msg;
                }

            }

            return View();
        }

        [HttpPost]
        [Route("Home/SetLanguage")]
        public ActionResult SetLanguage(BarraSuperiorViewModel model)
        {
            string Language = "";
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    if (model._userId != 0) 
                    {
                        var _user = _userRepository.GetByIdAsync(model._userId).Result;
                        if (_user != null) {
                            _user.DefaultLanguage = model._Language;
                            _userRepository.Update(_user);
                            Sucess = _userRepository.Save();

                            Language = _user.DefaultLanguage;
                            this._autenticacaoHelper.GravaSessao(valor: StringUtils.Base64Encode("SA" + "\\" + _user.Login + "\\" + _user.DefaultSiteId + "|" + GetSite(_user.DefaultSiteId) + "\\" + _user.Name + "\\" + Environment.MachineName.ToString() + "\\" + Language + "\\" + _user.Id));

                            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Language)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                        }                        
                    }
                    
                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = Language, Sucesso = Sucess, Mensagem = (Sucess ? "Sucesso" : "Erro"), Status = (Sucess ? 1 : 2) };
                    return Json(retornoWS, new JsonSerializerSettings());
                }
                else
                {
                    retornoWS = new RetornoWS { Sucesso = false, Mensagem = AddErrors() };
                    return Json(retornoWS, new JsonSerializerSettings());
                }
            }
            catch (Exception ex)
            {
                retornoWS = new RetornoWS { Sucesso = false, Mensagem = ex.Message };
                return Json(retornoWS, new JsonSerializerSettings());
            }
        }

        [HttpPost]
        [Route("Home/SetSite")]
        public ActionResult SetSite(BarraSuperiorViewModel model)
        {
            string siteName = "";
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    if (model._userId != 0)
                    {
                        var _user = _userRepository.GetByIdAsync(model._userId).Result;
                        if (_user != null)
                        {
                            _user.DefaultSiteId = model._siteId;
                            _userRepository.Update(_user);
                            Sucess = _userRepository.Save();

                            siteName = GetSite(model._siteId);
                            this._autenticacaoHelper.GravaSessao(valor: StringUtils.Base64Encode("SA" + "\\" + _user.Login + "\\" + model._siteId + "|" + siteName + "\\" + _user.Name + "\\" + Environment.MachineName.ToString() + "\\" + _user.DefaultLanguage + "\\" + _user.Id));
                        }
                    }

                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = siteName, Sucesso = Sucess, Mensagem = (Sucess ? "Sucesso" : "Erro"), Status = (Sucess ? 1 : 2) };
                    return Json(retornoWS, new JsonSerializerSettings());
                }
                else
                {
                    retornoWS = new RetornoWS { Sucesso = false, Mensagem = AddErrors() };
                    return Json(retornoWS, new JsonSerializerSettings());
                }
            }
            catch (Exception ex)
            {
                retornoWS = new RetornoWS { Sucesso = false, Mensagem = ex.Message };
                return Json(retornoWS, new JsonSerializerSettings());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetSite(int siteId)
        {
            return (siteId == 0 ? "" : _siteRepository.GetByIdAsync(siteId).Result.Name);
        }

    }
}
