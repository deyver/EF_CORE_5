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
using System.Text;

namespace MPQ.Controllers
{
    public class GoalCustomerRangeController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGoalCustomerRangeRepository _goalCustomerRangeRepository;
        private readonly IGoalCustomerRepository _goalCustomerRepository;
        private readonly IComparisonOperatorRepository _comparisonOperatorRepository;        
        const string sistema = "Faixa de Meta Cliente";

        #region Propriedades e Construtor

        public GoalCustomerRangeController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IGoalCustomerRangeRepository goalCustomerRangeRepository,
            IGoalCustomerRepository goalCustomerRepository,
            IComparisonOperatorRepository comparisonOperatorRepository,            
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._goalCustomerRangeRepository = goalCustomerRangeRepository;
            this._goalCustomerRepository = goalCustomerRepository;
            this._comparisonOperatorRepository = comparisonOperatorRepository;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        [Route("GoalCustomerRange/GetAllGoalCustomerRange")]
        public JsonResult GetAllGoalCustomerRange()
        {
            var _goalCustomerRange = _goalCustomerRangeRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, GoalCustomer = f.GoalCustomer.Name, Name = f.Name, Value = f.Value, Color = f.Color, Operator = f.Operator.Name });
            return Json(_goalCustomerRange);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListGoalCustomers()
        {
            try
            {
                var _goalCustomer = _goalCustomerRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _goalCustomer.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.GoalCustomer = _goalCustomer;
            }
            catch (Exception)
            {
                ViewBag.GoalCustomer = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListComparisonOperators()
        {
            try
            {
                var _comparisonOperator = _comparisonOperatorRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _comparisonOperator.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Operator = _comparisonOperator;
            }
            catch (Exception)
            {
                ViewBag.Operator = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListGoalCustomers();
            LoadDropDownListComparisonOperators();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(GoalCustomerRangeViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _goalCustomerRangeRepository.Add(new GoalCustomerRange { GoalCustomerId = model.GoalCustomerId, Name = model.Name, Value = model.Value, Color = model.Color, OperatorId = model.OperatorId });
                    Sucess = _goalCustomerRangeRepository.Save();

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
            LoadDropDownListGoalCustomers();
            LoadDropDownListComparisonOperators();

            var _goalCustomerRange = _goalCustomerRangeRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new GoalCustomerRangeViewModel() {  Id = _goalCustomerRange.Id, GoalCustomerId = _goalCustomerRange.GoalCustomerId, Name = _goalCustomerRange.Name, Value = _goalCustomerRange.Value, Color = _goalCustomerRange.Color, OperatorId = _goalCustomerRange.OperatorId });
        }

        [HttpPost]
        public ActionResult Update(GoalCustomerRangeViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _goalCustomerRange = new GoalCustomerRange { Id = model.Id, GoalCustomerId = model.GoalCustomerId, Name = model.Name, Value = model.Value, Color = model.Color, OperatorId = model.OperatorId };
                    _goalCustomerRangeRepository.Update(_goalCustomerRange); 
                    Sucess = _goalCustomerRangeRepository.Save();

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
                var _goalCustomerRange = _goalCustomerRangeRepository.GetByIdAsync(registro).Result;
                if ((_goalCustomerRange.Id == registro) && (registro != 0)) 
                {
                    _goalCustomerRangeRepository.Delete(_goalCustomerRange);
                    Sucess = _goalCustomerRangeRepository.Save();
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