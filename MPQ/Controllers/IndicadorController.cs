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
    public class IndicadorController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IIndicatorRepository _indicatorRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;
        const string sistema = "Indicador";

        #region Propriedades e Construtor

        public IndicadorController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IIndicatorRepository indicatorRepository,
            ISiteRepository siteRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._indicatorRepository = indicatorRepository;
            this._siteRepository = siteRepository;
            this._mapper = mapper;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region Consultas    
       
        [Route("Indicador/GetAllIndicator")]
        public async Task<JsonResult> GetAllIndicator(string dominio, string usuario, Guid IdDominio)
        {
            var user = await _indicatorRepository.GetAllAsync();            
            return Json(user);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListSite()
        {
            try
            {
                var site = _siteRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                site.Insert(index: 0, item: new SelectListItem(text: "", value: ""));
                ViewBag.Site = site;
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
                List<SelectListItem> status = new List<SelectListItem>() { new SelectListItem { Text = "", Value = "" }, new SelectListItem { Text = "Ativo", Value = "1" }, new SelectListItem { Text = "Inativo", Value = "2" } };
                ViewBag.Status = status;
            }
            catch (Exception)
            {
                ViewBag.Site = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(IndicadorViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _indicatorRepository.Add(new Indicator { Id = 0, Name = model.Name });
                    _indicatorRepository.Save();
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

        public ActionResult Update(int registro)
        {
            var user = _indicatorRepository.GetByIdAsync(registro).Result;
            return PartialView("_Update", new IndicadorViewModel() { Id = user.Id, Name = user.Name });
        }

        [HttpPost]
        public ActionResult Update(IndicadorViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _indicatorRepository.Update(new Indicator { Id = model.Id, Name = model.Name });
                    _indicatorRepository.Save();
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
        public JsonResult Delete(int registro)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            try
            {
                var company = _indicatorRepository.GetByIdAsync(registro).Result;
                if ((company.Id > 0))
                {
                    if (company.GoalStfs.Count() > 0) // SÓ DELETA EMPRESAS SEM VÍNCULO COM METASTF (GoalSTFs) (FK)
                    {
                        retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados à Meta!", Status = 2 };
                        return Json(retornoWS, new JsonSerializerSettings());
                    }
                    _indicatorRepository.Delete(company);
                    Sucess = _indicatorRepository.Save();
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