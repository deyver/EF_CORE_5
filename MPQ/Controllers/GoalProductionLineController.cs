using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using MPQ.Custom.Attribute;
using MPQ.Data.Repositories;
using MPQ.Domain;
using MPQ.Helpers;
using MPQ.Models;
using MPQ.Models.WebServices;
using MPQ.Utils.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPQ.Controllers
{
    public class GoalProductionLineController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGoalProductionLineRepository _goalProductionLineRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProductionLineRepository _productionLineRepository;
        private readonly IProductionAreaRepository _productionAreaRepository;
        private readonly IGoalProductionLineRangeRepository _goalProductionLineRangeRepository;
        private readonly IComparisonOperatorRepository _comparisonRepository;
        const string sistema = "Meta de Linha de Produção";

        #region Propriedades e Construtor

        public GoalProductionLineController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IGoalProductionLineRepository goalProductionLineRepository,
            ISiteRepository siteRepository,
            ICustomerRepository customerRepository,
            IProjectRepository projectRepository,
            IProductionLineRepository productionLineRepository,
            IProductionAreaRepository productionAreaRepository,
            IGoalProductionLineRangeRepository goalProductionLineRangeRepository,
            IComparisonOperatorRepository comparisonOperatorRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._goalProductionLineRepository = goalProductionLineRepository;
            this._siteRepository = siteRepository;
            this._customerRepository = customerRepository;
            this._projectRepository = projectRepository;
            this._productionLineRepository = productionLineRepository;
            this._productionAreaRepository = productionAreaRepository;
            this._goalProductionLineRangeRepository = goalProductionLineRangeRepository;
            _comparisonRepository = comparisonOperatorRepository;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize()]
        public ActionResult Edit(int id)
        {
            LoadDropDownListSites();
            LoadDropDownListCustomers();
            LoadDropDownListProjects();
            LoadDropDownListProductionLines();
            LoadDropDownListProductionAreas();
            LoadDropDownListComparisonOperator();

            GoalProductionLine goalProductionLine = _goalProductionLineRepository.GetByIdAsync(id).Result;
            GoalProductionLineViewModel viewModel = new GoalProductionLineViewModel()
            {
                Id = goalProductionLine.Id,
                SiteId = goalProductionLine.SiteId,
                CustomerId = goalProductionLine.CustomerId,
                ProjectId = goalProductionLine.ProjectId,
                ProductionLineId = goalProductionLine.ProductionLineId,
                StfNumber = goalProductionLine.StfNumber,
                Target = goalProductionLine.Target,
                ProductionAreaId = goalProductionLine.ProductionAreaId,
                GoalProductionLineRanges = _goalProductionLineRangeRepository.GetByGoalProductionLineIdAsync(goalProductionLine.Id).Result
            };

            return View(viewModel);
        }

        [CustomAuthorize()]
        [HttpPost]
        public ActionResult RemoveRange(int pGoalProductionLineId, int pGoalProductionLineRangeId)
        {
            try
            {
                GoalProductionLineRange range = _goalProductionLineRangeRepository.GetByIdAsync(pGoalProductionLineRangeId).Result;
                if (range != null)
                {
                    try
                    {
                        _goalProductionLineRangeRepository.Delete(range);
                        _goalProductionLineRangeRepository.Save();

                        return RedirectToAction("Edit", new { id = pGoalProductionLineId });
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                    throw new Exception("Record was not found.");

            }
            catch (Exception)
            {
                return RedirectToAction("Edit", new { id = pGoalProductionLineId });
            }
        }

        [CustomAuthorize()]
        [HttpPost]
        public ActionResult AddRange(IFormCollection collection)
        {
            int goalProductionLineId = int.Parse(collection["GoalProductionLineId"].ToString());
            string name = collection["Name"].ToString();
            int operatorId = int.Parse(collection["OperatorId"].ToString());
            decimal value = decimal.Parse(collection["Value"].ToString());
            string color = collection["Color"].ToString();

            GoalProductionLineRange range = new GoalProductionLineRange()
            {
                GoalProductionLineId = goalProductionLineId,
                Name = name,
                OperatorId = operatorId,
                Value = value,
                Color = color
            };

            _goalProductionLineRangeRepository.Add(range);
            _goalProductionLineRangeRepository.Save();

            return RedirectToAction("Edit", new { id = goalProductionLineId });
        }

        #region Consultas

        [Route("GoalProductionLine/GetAllGoalProductionLine")]
        public JsonResult GoalProductionLine()
        {
            var _goalProductionLine = _goalProductionLineRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Site = f.Site.Name, Customer = f.Customer.Name, Project = f.Project.Name, ProductionLine = f.ProductionLine.Name, StfNumber = f.StfNumber, Target = string.Format("{0:N2}", f.Target), ProductionArea = f.ProductionArea.Name });
            return Json(_goalProductionLine);
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

        public void LoadDropDownListProductionLines()
        {
            try
            {
                var _productionLine = _productionLineRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _productionLine.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.ProductionLine = _productionLine;
            }
            catch (Exception)
            {
                ViewBag.ProductionLine = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListProductionAreas()
        {
            try
            {
                var _productionArea = _productionAreaRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _productionArea.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.ProductionArea = _productionArea;
            }
            catch (Exception)
            {
                ViewBag.ProductionArea = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListComparisonOperator()
        {
            try
            {
                var _operators = _comparisonRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _operators.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Operator = _operators;
            }
            catch (Exception)
            {
                ViewBag.Operator = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSites();
            LoadDropDownListCustomers();
            LoadDropDownListProjects();
            LoadDropDownListProductionLines();
            LoadDropDownListProductionAreas();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(GoalProductionLineViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _goalProductionLineRepository.Add(new GoalProductionLine { SiteId = model.SiteId, CustomerId = model.CustomerId, ProjectId = model.ProjectId, ProductionLineId = model.ProductionLineId, StfNumber = model.StfNumber, Target = model.Target, ProductionAreaId = model.ProductionAreaId });
                    Sucess = _goalProductionLineRepository.Save();

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
            LoadDropDownListProjects();
            LoadDropDownListProductionLines();
            LoadDropDownListProductionAreas();

            var _goalProductionLine = _goalProductionLineRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new GoalProductionLineViewModel() { Id = _goalProductionLine.Id, SiteId = _goalProductionLine.SiteId, CustomerId = _goalProductionLine.CustomerId, ProjectId = _goalProductionLine.ProjectId, ProductionLineId = _goalProductionLine.ProductionLineId, StfNumber = _goalProductionLine.StfNumber, Target = _goalProductionLine.Target, ProductionAreaId = _goalProductionLine.ProductionAreaId });
        }

        [HttpPost]
        public ActionResult Update(GoalProductionLineViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _goalProductionLine = new GoalProductionLine { Id = model.Id, SiteId = model.SiteId, CustomerId = model.CustomerId, ProjectId = model.ProjectId, ProductionLineId = model.ProductionLineId, StfNumber = model.StfNumber, Target = model.Target, ProductionAreaId = model.ProductionAreaId };
                    _goalProductionLineRepository.Update(_goalProductionLine); 
                    Sucess = _goalProductionLineRepository.Save();

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
        public ActionResult Edit(GoalProductionLineViewModel model)
        {
            bool Sucess = false;


            try
            {
                if (ModelState.IsValid)
                {
                    var _goalProductionLine = new GoalProductionLine { Id = model.Id, SiteId = model.SiteId, CustomerId = model.CustomerId, ProjectId = model.ProjectId, ProductionLineId = model.ProductionLineId, StfNumber = model.StfNumber, Target = model.Target, ProductionAreaId = model.ProductionAreaId };
                    _goalProductionLineRepository.Update(_goalProductionLine);
                    Sucess = _goalProductionLineRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    LoadDropDownListSites();
                    LoadDropDownListCustomers();
                    LoadDropDownListProjects();
                    LoadDropDownListProductionLines();
                    LoadDropDownListProductionAreas();

                    return View();
                }
            }
            catch (Exception ex)
            {
                LoadDropDownListSites();
                LoadDropDownListCustomers();
                LoadDropDownListProjects();
                LoadDropDownListProductionLines();
                LoadDropDownListProductionAreas();

                return View();
            }
        }

        [HttpPost]
        public JsonResult Delete(int registro)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            try
            {
                var _goalProductionLine = _goalProductionLineRepository.GetByIdAsync(registro).Result;
                if ((_goalProductionLine.Id == registro) && (registro != 0)) 
                {
                    _goalProductionLineRepository.Delete(_goalProductionLine);
                    Sucess = _goalProductionLineRepository.Save();
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