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

namespace MPQ.Controllers
{
    public class SiteController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IUserRepository _userRepository;
        private readonly IUserSiteRepository _userSiteRepository;

        private readonly ISiteRepository _siteRepository;
        private readonly ICompanyRepository _empresaRepository;

        private readonly IMapper _mapper;
        const string sistema = "Site";

        #region Propriedades e Construtor

        public SiteController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IUserRepository userRepository,
            ISiteRepository siteRepository,
            ICompanyRepository empresaRepository,
            IUserSiteRepository userSiteRepository,

            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._userRepository = userRepository;
            this._userSiteRepository = userSiteRepository;
            this._siteRepository = siteRepository;
            this._empresaRepository = empresaRepository;
            this._mapper = mapper;
        }
        #endregion

        //[Custom.Attribute.CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        [Route("Site/GetAllSite")]
        public JsonResult GetAllSite(string dominio, string usuario, Guid IdDominio)
        {
            var site = _siteRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Name = f.Name, Empresa = GetCompany(f.CompanyId) });
            return Json(site);
        }

        private string GetCompany(int companyId)
        {
            return (companyId == 0 ? "" : _empresaRepository.GetByIdAsync(companyId).Result.Name);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListEmpresa()
        {
            try
            {
                var site = _empresaRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                site.Insert(index: 0, item: new SelectListItem(text: "", value: ""));
                ViewBag.Empresa = site;
            }
            catch (Exception)
            {
                ViewBag.Empresa = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListEmpresa();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(SiteViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _siteRepository.Add(new Site { Id = 0, Name = model.Name, CompanyId = model.CompanyId });
                    _siteRepository.Save();

                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = true, Mensagem = "Sucesso", Status = 1 };
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
            LoadDropDownListEmpresa();

            var site = _siteRepository.GetByIdAsync(registro).Result;
            return PartialView("_Update", new SiteViewModel() {Id = site.Id, Name = site.Name, CompanyId = site.CompanyId });
        }

        [HttpPost]
        public ActionResult Update(SiteViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _siteRepository.Update(new Site { Id = model.Id, Name = model.Name, CompanyId = model.CompanyId });
                    _siteRepository.Save();

                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = true, Mensagem = "Sucesso", Status = 1 };
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

        [HttpPost]
        public JsonResult Delete(int registro, string dominio)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            try
            {
                var _usersite = _userSiteRepository.GetBySiteIdAsync(registro).Result;
                var _company = _empresaRepository.GetBySiteIdAsync(dominio).Result;

                var site = _siteRepository.GetByIdAsync(registro).Result;

                if (_company.Count > 0)
                {
                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados a Empresa!", Status = 2 };
                    return Json(retornoWS, new JsonSerializerSettings());
                }

                if (_usersite.Count > 0) 
                {
                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados a UserSite!", Status = 2 };
                    return Json(retornoWS, new JsonSerializerSettings());
                }

                _siteRepository.Delete(site);
                _siteRepository.Save();

                retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = true, Mensagem = "Sucesso", Status = 1 };
                return Json(retornoWS, new JsonSerializerSettings());
            }
            catch (Exception ex)
            {
                retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = ex.Message.ToString(), Status = 2 };
                return Json(retornoWS, new JsonSerializerSettings());
            }
        }
        #endregion
    }
}