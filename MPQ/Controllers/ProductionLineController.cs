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
    public class ProductionLineController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IProjectRepository _projectRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductionAreaRepository _productionAreaRepository;

        private readonly IProductionLineRepository _productionLineRepository;
        private readonly IGoalProductionLineRepository _goalProductionLineRepository;
        const string sistema = "Projeto";

        #region Propriedades e Construtor

        public ProductionLineController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IProjectRepository projectRepository,
            ISiteRepository siteRepository,
            ICustomerRepository customerRepository,
            IProductionLineRepository productionLineRepository,
            IGoalProductionLineRepository goalProductionLineRepository,
            IProductionAreaRepository productionAreaRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._projectRepository = projectRepository;
            this._siteRepository = siteRepository;
            this._customerRepository = customerRepository;
            this._productionAreaRepository = productionAreaRepository;
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
       
        [Route("ProductionLine/GetAllProductionLine")]
        public JsonResult GetAllProductionLine()
        {
            var _productionLine = _productionLineRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Name = f.Name, Customer = f.Customer.Name, Site = f.Site.Name, Area = f.ProductionArea.Name, Project = f.Project.Name });
            return Json(_productionLine);
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

        public void LoadDropDownListProductionAreas()
        {
            try
            {
                var _productionArea = _productionAreaRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _productionArea.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Area = _productionArea;
            }
            catch (Exception)
            {
                ViewBag.Area = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListProjects()
        {
            try
            {
                var _project = _projectRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _project.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Project = _project;
            }
            catch (Exception)
            {
                ViewBag.Project = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSites();
            LoadDropDownListCustomers();
            LoadDropDownListProductionAreas();
            LoadDropDownListProjects();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(ProductionLineViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _productionLineRepository.Add(new ProductionLine { Name = model.Name, CustomerId = model.CustomerId, SiteId = model.SiteId, ProductionAreaId = model.ProductionAreaId, ProjectId = model.ProjectId });
                    Sucess = _productionLineRepository.Save();

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
            LoadDropDownListProductionAreas();
            LoadDropDownListProjects();

            var _productionLine = _productionLineRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new ProductionLineViewModel() { Id = _productionLine.Id, Name = _productionLine.Name, CustomerId = _productionLine.CustomerId, SiteId = _productionLine.SiteId, ProductionAreaId = _productionLine.ProductionAreaId, ProjectId = _productionLine.ProjectId ?? 0 });
        }

        [HttpPost]
        public ActionResult Update(ProductionLineViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {                    
                    var _productionLine = new ProductionLine { Id = model.Id, Name = model.Name, CustomerId = model.CustomerId, SiteId = model.SiteId, ProductionAreaId = model.ProductionAreaId, ProjectId = model.ProjectId };
                    _productionLineRepository.Update(_productionLine); 
                    Sucess = _productionLineRepository.Save();

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
                var _productionLine = _productionLineRepository.GetByIdAsync(registro).Result;
                if ((_productionLine.Id == registro) && (registro != 0)) 
                {
                    var _goalProductionLine = _goalProductionLineRepository.GetByProjectIdAsync(registro).Result;

                    if (_goalProductionLine.Count > 0)
                    {
                        retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados a Meta de Linha de Produção!", Status = 2 };
                        return Json(retornoWS, new JsonSerializerSettings());
                    }

                    _productionLineRepository.Delete(_productionLine);
                    Sucess = _productionLineRepository.Save();
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