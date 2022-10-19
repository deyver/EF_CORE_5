using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using MPQ.Data.Repositories;
using MPQ.Helpers;
using MPQ.Models;
using MPQ.Models.WebServices;
using MPQ.Utils.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Collections;
using MPQ.Domain;
using MPQ.Custom.Attribute;

namespace MPQ.Controllers
{
    public class UsuarioController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUserRepository _userRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IUserSiteRepository _userSiteRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;
        const string sistema = "Usuario";

        #region Propriedades e Construtor

        public UsuarioController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUserRepository userRepository,
            ISiteRepository siteRepository,
            IUserSiteRepository userSiteRepository,
            IGroupRepository groupRepository,
            IUserGroupRepository userGroupRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._userRepository = userRepository;
            this._siteRepository = siteRepository;
            this._userSiteRepository = userSiteRepository;
            this._groupRepository = groupRepository;
            this._userGroupRepository = userGroupRepository;
            this._mapper = mapper;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        public JsonResult ListaUsuarioAds(string searchField, string searchString, string filtro, string dominio, Guid IdDominio)
        {
            var montaJson = ListaUsuarios(searchField, searchString, filtro, dominio, IdDominio);

            return Json(montaJson);
        }

        [SupportedOSPlatform("windows")]
        private IEnumerable<dynamic> ListaUsuarios(string searchField, string searchString, string filtro, string dominio, Guid IdDominio)
        {
            //var infoDominio = ConsultarDominioPorId(IdDominio).Result;
            var listaRetorno = new List<dynamic>();

            try
            {
                var sortOption = new SortOption("samaccountname", SortDirection.Ascending);
                var directoryEntry = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);

                var diretorioDominio = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
                var dadosDominio = new DirectorySearcher(diretorioDominio,
                    "(&(objectCategory=person)(objectClass=user)(" + searchField + "=" + searchString + filtro +
                    "))", new[] { "samaccountname" })
                {
                    Sort = sortOption,
                    SizeLimit = 100
                };

                var valores = dadosDominio.FindAll();
                foreach (var entry in from SearchResult sr in valores select sr.GetDirectoryEntry() into entry where entry.Properties.Contains("displayname") where entry.Properties["displayname"].Count > 0 select entry)
                {
                    listaRetorno.Add(new
                    {
                        value = entry.Properties["samaccountname"][0].ToString(),
                        label = entry.Properties["samaccountname"][0] + " - " + entry.Properties["displayname"][0]
                    });
                }
            }
            catch (Exception e)
            {
                listaRetorno.Add(new { value = string.Empty, label = "Erro" });
            }

            return listaRetorno;
        }

        [Route("Usuario/GetAllUser")]
        public JsonResult GetAllUser(string dominio, string usuario, Guid IdDominio)
        {
            var user = _userRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Login = f.Login, Name = f.Name, DefaultLanguage = f.DefaultLanguage, Status = (f.Status == 1 ? "Ativo" : "Inativo") });
            return Json(user);
        }


        [SupportedOSPlatform("windows")]
        public JsonResult ConsultaNomeUsuarioPorMatricula(string dominio, string usuario, Guid IdDominio)
        {
            if (dominio == null || usuario == null)
            {
                return Json(new List<dynamic>() { new { value = "" } });
            }

            //var infoDominio = ConsultarDominioPorId(IdDominio).Result;
            var listaRetorno = new List<dynamic>();

            var sortOption = new SortOption("samaccountname", SortDirection.Ascending);
            var directoryEntry = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var strDomain = directoryEntry.Properties["defaultNamingContext"].Value.ToString();

            var diretorioDominio = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var dadosDominio = new DirectorySearcher(diretorioDominio,
            $"(&(objectCategory=person)(objectClass=user)(sAMAccountName={usuario}))", new[] { "samaccountname" })
            {
                Sort = sortOption,
                SizeLimit = 100
            };

            var valores = dadosDominio.FindAll();
            foreach (var entry in from SearchResult sr in valores select sr.GetDirectoryEntry() into entry where entry.Properties.Contains("displayname") where entry.Properties["displayname"].Count > 0 select entry)
            {
                listaRetorno.Add(new
                {
                    value = entry.Properties["displayname"][0]
                });
            }

            return Json(listaRetorno);
        }

        [SupportedOSPlatform("windows")]
        private bool ValidarExisteUsuarioAD(string usuario)
        {
            var sortOption = new SortOption("samaccountname", SortDirection.Ascending);
            var directoryEntry = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var strDomain = directoryEntry.Properties["defaultNamingContext"].Value.ToString();

            var de = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var dm = new DirectorySearcher(de,
                $"(&(objectCategory=person)(objectClass=user)(sAMAccountName={usuario}))", new[] { "samaccountname" })
            {
                Sort = sortOption,
                SizeLimit = 100
            };

            var valores = dm.FindAll();

            return valores.Count > 0;
        }

        #endregion

        #region CRUD

        public void LoadDropDownListSite()
        {
            try
            {
                var site = _siteRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                site.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Site = site;
            }
            catch (Exception)
            {
                ViewBag.Site = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListGroup()
        {
            try
            {
                var group = _groupRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                group.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Group = group;
            }
            catch (Exception)
            {
                ViewBag.Site = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListStatus()
        {
            try
            {
                List<SelectListItem> status = new List<SelectListItem>() { new SelectListItem { Text = "", Value = "0" }, new SelectListItem { Text = "Ativo", Value = "1" }, new SelectListItem { Text = "Inativo", Value = "2" } };
                ViewBag.Status = status;
            }
            catch (Exception)
            {
                ViewBag.Site = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListIdioma()
        {
            try
            {
                List<SelectListItem> idioma = new List<SelectListItem>();
                idioma.Insert(index: 0, item: new SelectListItem(text: "Português (Brasil)", value: "pt-BR"));
                idioma.Insert(index: 1, item: new SelectListItem(text: "Espanhol", value: "es"));
                ViewBag.Idioma = idioma;
            }
            catch (Exception)
            {
                ViewBag.Idioma = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSite();
            LoadDropDownListStatus();
            LoadDropDownListGroup();
            LoadDropDownListIdioma();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(UsuarioViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            List<UserSite> userSite;
            List<UserGroup> userGroup;

            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Add(new User { Login = model.Login, Name = model.Name, DefaultSiteId = model.DefaultSiteId, DefaultLanguage = model.DefaultLanguage, Status = model.Status });
                    Sucess = _userRepository.Save();

                    var user = _userRepository.GetByLoginAsync(model.Login).Result;

                    userSite = _userSiteRepository.GetByUserIdAsync(user.Id).Result.ToList();
                    if (userSite != null && userSite.Count > 0) {
                        _userSiteRepository.Delete(userSite);
                        _userSiteRepository.Save();
                    }

                    userGroup = _userGroupRepository.GetByUserIdAsync(user.Id).Result.ToList();
                    if (userGroup != null && userGroup.Count > 0) {
                        _userGroupRepository.Delete(userGroup);
                        _userGroupRepository.Save();
                    }

                    userSite = model.UserSiteId?.Where(w => w > 0).Select(s => new UserSite { UserId = user.Id, SiteId = (int)s }).ToList();
                    if (userSite != null && userSite.Count > 0) {
                        _userSiteRepository.Add(userSite);
                        _userSiteRepository.Save();
                    }

                    userGroup = model.UserGroupId?.Where(w => w > 0).Select(s => new UserGroup { UserId = user.Id, GroupId = (int)s }).ToList();
                    if (userGroup != null && userGroup.Count > 0) {
                        _userGroupRepository.Add(userGroup);
                        _userGroupRepository.Save();
                    }
                    
                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = Sucess, Mensagem = (Sucess ? "Sucesso" : "Erro"), Status = (Sucess ? 1 : 2) };
                    return Json(retornoWS, new JsonSerializerSettings());
                }
                else
                {
                    retornoWS = new RetornoWS { Sucesso = false, Mensagem = AddErrors() };
                    return Json(retornoWS, new JsonSerializerSettings());
                }
            }
            catch (Exception exc)
            {
                retornoWS = new RetornoWS { Sucesso = false, Mensagem = exc.Message };
                return Json(retornoWS, new JsonSerializerSettings());
            }
        }

        public ActionResult Update(int registro, string dominio)
        {
            LoadDropDownListSite();
            LoadDropDownListStatus();
            LoadDropDownListGroup();
            LoadDropDownListIdioma();

            var user = _userRepository.GetByLoginAsync(dominio).Result;
            var userSite = _userSiteRepository.GetByUserIdAsync(user.Id).Result.Select(s => s.SiteId).ToList();
            var userGroup = _userGroupRepository.GetByUserIdAsync(user.Id).Result.Select(s => s.GroupId).ToList();
            return PartialView("_Update", new UsuarioViewModel() { Id = user.Id, Login = user.Login, Name = user.Name, DefaultSiteId = user.DefaultSiteId, DefaultLanguage = user.DefaultLanguage, Status = user.Status, UserSiteId = userSite, UserGroupId = userGroup });
        }

        [HttpPost]
        public ActionResult Update(UsuarioViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            List<UserSite> userSite;
            List<UserGroup> userGroup;

            try
            {
                if (ModelState.IsValid)
                {
                    var _user = new User { Id = model.Id, Login = model.Login, Name = model.Name, DefaultSiteId = model.DefaultSiteId, DefaultLanguage = model.DefaultLanguage, Status = model.Status };
                    _userRepository.Update(_user); 
                    Sucess = _userRepository.Save();

                    userSite = _userSiteRepository.GetByUserIdAsync(model.Id).Result.ToList();
                    if (userSite != null && userSite.Count > 0) {
                        _userSiteRepository.Delete(userSite);
                        _userSiteRepository.Save();
                    }

                    userGroup = _userGroupRepository.GetByUserIdAsync(model.Id).Result.ToList();
                    if (userGroup != null && userGroup.Count > 0) {
                        _userGroupRepository.Delete(userGroup);
                        _userGroupRepository.Save();
                    }

                    userSite = model.UserSiteId?.Where(w => w > 0).Select(s => new UserSite { UserId = _user.Id, SiteId = (int)s }).ToList();
                    if (userSite != null && userSite.Count > 0)
                    {
                        _userSiteRepository.Add(userSite);
                        _userSiteRepository.Save();
                    }

                    userGroup = model.UserGroupId?.Where(w => w > 0).Select(s => new UserGroup { UserId = _user.Id, GroupId = (int)s }).ToList();
                    if (userGroup != null && userGroup.Count > 0)
                    {
                        _userGroupRepository.Add(userGroup);
                        _userGroupRepository.Save();
                    }

                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = Sucess, Mensagem = (Sucess ? "Sucesso" : "Erro"), Status = (Sucess ? 1 : 2) };                    
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
        public JsonResult Delete(int registro, string dominio)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            var user = _userRepository.GetByLoginAsync(dominio).Result;
            if ((user.Login == dominio) && (dominio.Trim() != "")) 
            {
                user.Status = 2; _userRepository.Update(user);
                Sucess = _userRepository.Save(); 
            }

            retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = Sucess, Mensagem = (Sucess ? "Sucesso" : "Erro"), Status = (Sucess ? 1 : 2) };
            return Json(retornoWS, new JsonSerializerSettings());            
        }

        #endregion
    }
}