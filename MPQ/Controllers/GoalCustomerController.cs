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
    public class GoalCustomerController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGoalCustomerRepository _goalCustomerRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IGoalCustomerRangeRepository _goalCustomerRangeRepository;
        private readonly IComparisonOperatorRepository _comparisonRepository;
        const string sistema = "Meta de Cliente";

        #region Propriedades e Construtor

        public GoalCustomerController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IGoalCustomerRepository goalCustomerRepository,
            ISiteRepository siteRepository,
            ICustomerRepository customerRepository,
            IGoalCustomerRangeRepository goalCustomerRangeRepository,
            IComparisonOperatorRepository comparisonOperatorRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._goalCustomerRepository = goalCustomerRepository;
            this._siteRepository = siteRepository;
            this._customerRepository = customerRepository;
            this._goalCustomerRangeRepository = goalCustomerRangeRepository;
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
            LoadDropDownListComparisonOperator();

            GoalCustomer goalCustomer = _goalCustomerRepository.GetByIdAsync(id).Result;
            GoalCustomerViewModel viewModel = new GoalCustomerViewModel()
            {
                Id = goalCustomer.Id,
                Name = goalCustomer.Name,
                SiteId = goalCustomer.SiteId,
                CustomerId = goalCustomer.CustomerId,
                UnitOfMeasurement = goalCustomer.UnitOfMeasurement,
                StfNumber = goalCustomer.StfNumber,
                GoalCustomerRanges = _goalCustomerRangeRepository.GetByGoalCustomerIdAsync(goalCustomer.Id).Result
            };

            return View(viewModel);
        }

        [CustomAuthorize()]
        [HttpPost]
        public ActionResult RemoveRange(int pGoalCustomerId, int pGoalCustomerRangeId)
        {
            try
            {
                GoalCustomerRange range = _goalCustomerRangeRepository.GetByIdAsync(pGoalCustomerRangeId).Result;
                if (range != null)
                {
                    try
                    {
                        _goalCustomerRangeRepository.Delete(range);
                        _goalCustomerRangeRepository.Save();

                        return RedirectToAction("Edit", new { id = pGoalCustomerId });
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
                return RedirectToAction("Edit", new { id = pGoalCustomerId });
            }
        }

        [CustomAuthorize()]
        [HttpPost]
        public ActionResult AddRange(IFormCollection collection)
        {
            int goalCustomerId = int.Parse(collection["GoalCustomerId"].ToString());
            string name = collection["Name"].ToString();
            int operatorId = int.Parse(collection["OperatorId"].ToString());
            decimal value = decimal.Parse(collection["Value"].ToString());
            string color = collection["Color"].ToString();

            GoalCustomerRange range = new GoalCustomerRange()
            {
                GoalCustomerId = goalCustomerId,
                Name = name,
                OperatorId = operatorId,
                Value = value,
                Color = color
            };

            _goalCustomerRangeRepository.Add(range);
            _goalCustomerRangeRepository.Save();

            return RedirectToAction("Edit", new { id = goalCustomerId });
        }

        #region Consultas

        [Route("GoalCustomer/GetAllGoalCustomer")]
        public JsonResult GetAllGoalCustomer()
        {
            var _goalCustomer = _goalCustomerRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Name = f.Name, Site = f.Site.Name, Customer = f.Customer.Name, UnitOfMeasurement = f.UnitOfMeasurement, StfNumber = f.StfNumber });
            return Json(_goalCustomer);
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
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(GoalCustomerViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    GoalCustomer goalCustomer = _goalCustomerRepository.Add(new GoalCustomer { Name = model.Name, SiteId = model.SiteId, CustomerId = model.CustomerId, UnitOfMeasurement = model.UnitOfMeasurement, StfNumber = model.StfNumber });
                    Sucess = _goalCustomerRepository.Save();

                    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = string.Format("ID:{0}", goalCustomer.Id), Sucesso = Sucess, Mensagem = (Sucess ? "Sucesso" : "Erro"), Status = (Sucess ? 1 : 2) };
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

        public ActionResult Update(int id)
        {
            LoadDropDownListSites();
            LoadDropDownListCustomers();

            var _goalCustomer = _goalCustomerRepository.GetByIdAsync(id).Result;            
            return PartialView("_Update", new GoalCustomerViewModel() {  Id = _goalCustomer.Id, Name = _goalCustomer.Name, SiteId = _goalCustomer.SiteId, CustomerId = _goalCustomer.CustomerId, UnitOfMeasurement = _goalCustomer.UnitOfMeasurement, StfNumber = _goalCustomer.StfNumber });
        }

        [HttpPost]
        public ActionResult Update(GoalCustomerViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _goalCustomer = new GoalCustomer { Id = model.Id, Name = model.Name, SiteId = model.SiteId, CustomerId = model.CustomerId, UnitOfMeasurement = model.UnitOfMeasurement, StfNumber = model.StfNumber };
                    _goalCustomerRepository.Update(_goalCustomer); 
                    Sucess = _goalCustomerRepository.Save();

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
        public ActionResult Edit(GoalCustomerViewModel model)
        {
            bool Sucess = false;
            

            try
            {
                if (ModelState.IsValid)
                {
                    var _goalCustomer = new GoalCustomer { Id = model.Id, Name = model.Name, SiteId = model.SiteId, CustomerId = model.CustomerId, UnitOfMeasurement = model.UnitOfMeasurement, StfNumber = model.StfNumber };
                    _goalCustomerRepository.Update(_goalCustomer);
                    Sucess = _goalCustomerRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    LoadDropDownListSites();
                    LoadDropDownListCustomers();
                    LoadDropDownListComparisonOperator();

                    return View();
                }
            }
            catch (Exception ex)
            {
                LoadDropDownListSites();
                LoadDropDownListCustomers();
                LoadDropDownListComparisonOperator();

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
                var _goalCustomer = _goalCustomerRepository.GetByIdAsync(registro).Result;
                if ((_goalCustomer.Id == registro) && (registro != 0)) 
                {
                    _goalCustomerRepository.Delete(_goalCustomer);
                    Sucess = _goalCustomerRepository.Save();
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