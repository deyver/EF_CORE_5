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
    public class MetaController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGoalRepository _goalRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;
        const string sistema = "Meta";

        #region Propriedades e Construtor

        public MetaController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IGoalRepository goalRepository,
            ISiteRepository siteRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._goalRepository = goalRepository;
            this._siteRepository = siteRepository;
            this._mapper = mapper;
        }
        #endregion

        //[Custom.Attribute.CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        [Route("Meta/GetAllGoal")]
        public async Task<JsonResult> GetAllGoal(string dominio, string usuario, Guid IdDominio)
        {
            var user = await _goalRepository.GetAllAsync();            
            return Json(user);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListGoal()
        {
            try
            {
                //var opcao = this._sharedLocalizer["SelectOpcao"];
                var meta = _goalRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                meta.Insert(index: 0, item: new SelectListItem(text: "", value: ""));
                ViewBag.Site = meta;
            }
            catch (Exception)
            {
                ViewBag.Goal = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListStatus()
        {
            try
            {
                //var opcao = this._sharedLocalizer["SelectOpcao"];
                List<SelectListItem> status = new List<SelectListItem>() { new SelectListItem { Text = "", Value = "" }, new SelectListItem { Text = "Ativo", Value = "1" }, new SelectListItem { Text = "Inativo", Value = "2" } };
                ViewBag.Status = status;
            }
            catch (Exception)
            {
                ViewBag.Goal = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            //LoadDropDownListSite();
            //LoadDropDownListStatus();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(MetaViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _goalRepository.Add(new Goal { Id = 0, Name = model.Name });
                    _goalRepository.Save();
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
            var user = _goalRepository.GetByIdAsync(registro).Result;
            return PartialView("_Update", new MetaViewModel() { Id = user.Id, Name = user.Name });
        }

        [HttpPost]
        public ActionResult Update(MetaViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _goalRepository.Update(new Goal { Id = model.Id, Name = model.Name });
                    _goalRepository.Save();
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
                var goal = _goalRepository.GetByIdAsync(registro).Result;
                if ((goal.Id > 0))
                {
                    //if (goal.MetaSTF.Equals(0)) // SÓ DELETA METAS SEM VÍNCULO COM METASTF (FK)
                    //{
                    //    retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados a MetaSTF!", Status = 2 };
                    //    return Json(retornoWS, new JsonSerializerSettings());
                    //}
                    _goalRepository.Delete(goal);
                    Sucess = _goalRepository.Save();
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