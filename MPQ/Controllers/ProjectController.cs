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
    public class ProjectController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IProjectRepository _projectRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductionLineRepository _productionLineRepository;
        private readonly IGoalProductionLineRepository _goalProductionLineRepository;
        const string sistema = "Projeto";

        #region Propriedades e Construtor

        public ProjectController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IProjectRepository projectRepository,
            ISiteRepository siteRepository,
            ICustomerRepository customerRepository,
            IProductionLineRepository productionLineRepository,
            IGoalProductionLineRepository goalProductionLineRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._projectRepository = projectRepository;
            this._siteRepository = siteRepository;
            this._customerRepository = customerRepository;
            this._productionLineRepository = productionLineRepository;
            this._goalProductionLineRepository = goalProductionLineRepository;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        [Route("Project/GetAllProject")]
        public JsonResult GetAllProject()
        {
            var _project = _projectRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Name = f.Name, Customer = f.Customer.Name, Site = f.Site.Name });
            return Json(_project);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListSites()
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

        public void LoadDropDownListCustomers()
        {
            try
            {
                var _customer = _customerRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _customer.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Customer = _customer;
            }
            catch (Exception)
            {
                ViewBag.Customer = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSites();
            LoadDropDownListCustomers();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(ProjectViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _projectRepository.Add(new Project { Name = model.Name, CustomerId = model.CustomerId, SiteId = model.SiteId });
                    Sucess = _projectRepository.Save();

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

        public ActionResult Update(int registro)
        {
            LoadDropDownListSites();
            LoadDropDownListCustomers();

            var _project = _projectRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new ProjectViewModel() { Id = _project.Id, Name = _project.Name, CustomerId = _project.CustomerId, SiteId = _project.SiteId });
        }

        [HttpPost]
        public ActionResult Update(ProjectViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _project = new Project { Id = model.Id, Name = model.Name, CustomerId = model.CustomerId, SiteId = model.SiteId };
                    _projectRepository.Update(_project); 
                    Sucess = _projectRepository.Save();

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
        public JsonResult Delete(int registro)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            try
            {
                var _project = _projectRepository.GetByIdAsync(registro).Result;
                if ((_project.Id == registro) && (registro != 0)) 
                {
                    var _productionLine = _productionLineRepository.GetByProjectIdAsync(registro).Result;
                    var _goalProductionLine = _goalProductionLineRepository.GetByProjectIdAsync(registro).Result;

                    if (_productionLine.Count > 0)
                    {
                        retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados a Linha de Produção!", Status = 2 };
                        return Json(retornoWS, new JsonSerializerSettings());
                    }

                    if (_goalProductionLine.Count > 0)
                    {
                        retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados a Meta de Linha de Produção!", Status = 2 };
                        return Json(retornoWS, new JsonSerializerSettings());
                    }

                    _projectRepository.Delete(_project);
                    Sucess = _projectRepository.Save();
                }

                retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = Sucess, Mensagem = (Sucess ? "Sucesso" : "Erro"), Status = (Sucess ? 1 : 2) };
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