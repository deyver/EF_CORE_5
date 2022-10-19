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
    public class GoalProductionLineRangeController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGoalProductionLineRangeRepository _goalProductionLineRangeRepository;
        private readonly IGoalProductionLineRepository _goalProductionLineRepository;
        private readonly IComparisonOperatorRepository _comparisonOperatorRepository;        
        const string sistema = "Faixa de Meta Linha de Produção";

        #region Propriedades e Construtor

        public GoalProductionLineRangeController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IGoalProductionLineRangeRepository goalProductionLineRangeRepository,
            IGoalProductionLineRepository goalProductionLineRepository,
            IComparisonOperatorRepository comparisonOperatorRepository,            
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._goalProductionLineRangeRepository = goalProductionLineRangeRepository;
            this._goalProductionLineRepository = goalProductionLineRepository;
            this._comparisonOperatorRepository = comparisonOperatorRepository;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        [Route("GoalProductionLineRange/GetAllGoalProductionLineRange")]
        public JsonResult GetAllGoalProductionLineRange()
        {
            var _goalProductionLine = _goalProductionLineRangeRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, ProductionLine = f.GoalProductionLine.ProductionLine.Name, Name = f.Name, Value = f.Value, Color = f.Color, Operator = f.Operator.Name });
            return Json(_goalProductionLine);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListGoalProductionLines()
        {
            try
            {
                var _goalProductionLine = _goalProductionLineRepository.GetAllAsync().Result.ToList().OrderBy(o => o.ProductionLine.Name).Select(s => new SelectListItem { Text = s.ProductionLine.Name, Value = s.Id.ToString() }).ToList();
                _goalProductionLine.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.GoalProductionLine = _goalProductionLine;
            }
            catch (Exception)
            {
                ViewBag.GoalProductionLine = new List<SelectListItem>();
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
            LoadDropDownListGoalProductionLines();
            LoadDropDownListComparisonOperators();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(GoalProductionLineRangeViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _goalProductionLineRangeRepository.Add(new GoalProductionLineRange { GoalProductionLineId = model.GoalProductionLineId, Name = model.Name, Value = model.Value, Color = model.Color, OperatorId = model.OperatorId });
                    Sucess = _goalProductionLineRangeRepository.Save();

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
            LoadDropDownListGoalProductionLines();
            LoadDropDownListComparisonOperators();

            var _goalProductionLineRange = _goalProductionLineRangeRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new GoalProductionLineRangeViewModel() {  Id = _goalProductionLineRange.Id, GoalProductionLineId = _goalProductionLineRange.GoalProductionLineId, Name = _goalProductionLineRange.Name, Value = _goalProductionLineRange.Value, Color = _goalProductionLineRange.Color, OperatorId = _goalProductionLineRange.OperatorId });
        }

        [HttpPost]
        public ActionResult Update(GoalProductionLineRangeViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _goalProductionLineRange = new GoalProductionLineRange { Id = model.Id, GoalProductionLineId  = model.GoalProductionLineId, Name = model.Name, Value = model.Value, Color = model.Color, OperatorId = model.OperatorId };
                    _goalProductionLineRangeRepository.Update(_goalProductionLineRange); 
                    Sucess = _goalProductionLineRangeRepository.Save();

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
                var _goalProductionLineRange = _goalProductionLineRangeRepository.GetByIdAsync(registro).Result;
                if ((_goalProductionLineRange.Id == registro) && (registro != 0)) 
                {
                    _goalProductionLineRangeRepository.Delete(_goalProductionLineRange);
                    Sucess = _goalProductionLineRangeRepository.Save();
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