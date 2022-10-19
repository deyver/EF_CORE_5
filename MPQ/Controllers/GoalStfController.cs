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
    public class GoalStfController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGoalStfRepository _goalStfRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IIndicatorRepository _indicatorRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IBusinessUnitRepository _businessUnitRepository;
        const string sistema = "Meta STF";

        #region Propriedades e Construtor

        public GoalStfController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IGoalStfRepository goalStfRepository,
            ISiteRepository siteRepository,
            IIndicatorRepository indicatorRepository,
            IGoalRepository goalRepository,
            IBusinessUnitRepository businessUnitRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._goalStfRepository = goalStfRepository;
            this._siteRepository = siteRepository;
            this._indicatorRepository = indicatorRepository;
            this._goalRepository = goalRepository;
            this._businessUnitRepository = businessUnitRepository;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        [Route("GoalStf/GetAllGoalStf")]
        public JsonResult GetAllGoalStf()
        {
            var _goalStf = _goalStfRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Site = f.Site.Name, Indicator = f.Indicator.Name, BusinessUnit = f.BusinessUnit.Name, Goal = f.Goal.Name, StfNumber = f.StfNumber, GeneralTarget = string.Format("{0:N2}", f.GeneralTarget) });
            return Json(_goalStf);
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

        public void LoadDropDownListIndicators()
        {
            try
            {
                var _indicator = _indicatorRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _indicator.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Indicator = _indicator;
            }
            catch (Exception)
            {
                ViewBag.Indicator = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListGoals()
        {
            try
            {
                var _goal = _goalRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _goal.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Goal = _goal;
            }
            catch (Exception)
            {
                ViewBag.Goal = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListBusinessUnits()
        {
            try
            {
                var _businessUnit = _businessUnitRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _businessUnit.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.BusinessUnit = _businessUnit;
            }
            catch (Exception)
            {
                ViewBag.BusinessUnit = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSites();
            LoadDropDownListIndicators();
            LoadDropDownListGoals();
            LoadDropDownListBusinessUnits();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(GoalStfViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _goalStfRepository.Add(new GoalStf {    SiteId = model.SiteId, IndicatorId = model.IndicatorId, GoalId = model.GoalId, BusinessUnitId = model.BusinessUnitId, StfNumber = model.StfNumber, GeneralTarget = model.GeneralTarget,
                                                            JanTarget = model.JanTarget, FebTarget = model.FebTarget, MarTarget = model.MarTarget, AprTarget = model.AprTarget, MayTarget = model.MayTarget, JunTarget = model.JunTarget,
                                                            JulTarget = model.JulTarget, AugTarget = model.AugTarget, SepTarget = model.SepTarget, OctTarget = model.OctTarget, NovTarget = model.NovTarget, DecTarget = model.DecTarget });
                    Sucess = _goalStfRepository.Save();

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
            LoadDropDownListIndicators();
            LoadDropDownListGoals();
            LoadDropDownListBusinessUnits();

            var _goalStf = _goalStfRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new GoalStfViewModel() {  Id = _goalStf.Id, SiteId = _goalStf.SiteId, IndicatorId = _goalStf.IndicatorId, GoalId = _goalStf.GoalId, BusinessUnitId = _goalStf.BusinessUnitId, StfNumber = _goalStf.StfNumber, GeneralTarget = _goalStf.GeneralTarget,
                                                                    JanTarget = _goalStf.JanTarget, FebTarget = _goalStf.FebTarget, MarTarget = _goalStf.MarTarget, AprTarget = _goalStf.AprTarget, MayTarget = _goalStf.MayTarget, JunTarget = _goalStf.JunTarget,
                                                                    JulTarget = _goalStf.JulTarget, AugTarget = _goalStf.AugTarget, SepTarget = _goalStf.SepTarget, OctTarget = _goalStf.OctTarget, NovTarget = _goalStf.NovTarget, DecTarget = _goalStf.DecTarget });
        }

        [HttpPost]
        public ActionResult Update(GoalStfViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _goalStf = new GoalStf {    Id = model.Id, SiteId = model.SiteId, IndicatorId = model.IndicatorId, GoalId = model.GoalId, BusinessUnitId = model.BusinessUnitId, StfNumber = model.StfNumber, GeneralTarget = model.GeneralTarget,
                                                    JanTarget = model.JanTarget, FebTarget = model.FebTarget, MarTarget = model.MarTarget, AprTarget = model.AprTarget, MayTarget = model.MayTarget, JunTarget = model.JunTarget,
                                                    JulTarget = model.JulTarget, AugTarget = model.AugTarget, SepTarget = model.SepTarget, OctTarget = model.OctTarget, NovTarget = model.NovTarget, DecTarget = model.DecTarget };
                    _goalStfRepository.Update(_goalStf); 
                    Sucess = _goalStfRepository.Save();

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
                var _goalStf = _goalStfRepository.GetByIdAsync(registro).Result;
                if ((_goalStf.Id == registro) && (registro != 0)) 
                {
                    _goalStfRepository.Delete(_goalStf);
                    Sucess = _goalStfRepository.Save();
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