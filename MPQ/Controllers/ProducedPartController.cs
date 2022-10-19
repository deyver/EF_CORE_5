using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
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
using System.Threading.Tasks;

namespace MPQ.Controllers
{
    public class ProducedPartController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IProducedPartRepository _producedPartRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IProductionAreaRepository _productionAreaRepository;
        private readonly IMapper _mapper;
        const string sistema = "Produced Part";

        #region Propriedades e Construtor

        public ProducedPartController(
            IConfiguration configuration,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IProducedPartRepository producedPartRepository,
            ISiteRepository siteRepository,
            IProductionAreaRepository productionAreaRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._producedPartRepository = producedPartRepository;
            this._siteRepository = siteRepository;
            this._productionAreaRepository = productionAreaRepository;
            this._mapper = mapper;
        }
        #endregion

        // GET: ProducedPartController
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
        [Route("ProducedPart/GetAll")]
        public async Task<JsonResult> GetAll()
        {
            var user = await _producedPartRepository.GetAllAsync();
            return Json(user);
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

        public void LoadDropDownListProductionAreas()
        {
            try
            {
                var productionArea = _productionAreaRepository.GetAllAsync().Result.ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                productionArea.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.ProductionArea = productionArea;
            }
            catch (Exception)
            {
                ViewBag.ProductionArea = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSites();
            LoadDropDownListProductionAreas();

            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(ProducedPartViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _producedPartRepository.Add(new ProducedPart {
                        Id = 0,
                        SiteId = model.SiteId,
                        ProductionAreaId = model.ProductionAreaId,
                        Year = model.Year,
                        WeekNumber = model.WeekNumber,
                        Quantity = model.Quantity
                    });
                    _producedPartRepository.Save();
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

        public ActionResult Update(int id)
        {
            var producedPart = _producedPartRepository.GetByIdAsync(id).Result;
            return PartialView("_Update", new ProducedPartViewModel() { 
                Id = producedPart.Id, 
                SiteId = producedPart.SiteId, 
                ProductionAreaId = producedPart.ProductionAreaId, 
                Year = producedPart.Year, 
                WeekNumber = producedPart.WeekNumber, 
                Quantity = producedPart.Quantity 
            });
        }

        [HttpPost]
        public ActionResult Update(ProducedPartViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _producedPartRepository.Update(new ProducedPart {
                        Id = model.Id,
                        SiteId = model.SiteId,
                        ProductionAreaId = model.ProductionAreaId,
                        Year = model.Year,
                        WeekNumber = model.WeekNumber,
                        Quantity = model.Quantity
                    });
                    _producedPartRepository.Save();
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
        public JsonResult Delete(int id)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            try
            {
                var company = _producedPartRepository.GetByIdAsync(id).Result;
                if ((company.Id > 0))
                {
                    _producedPartRepository.Delete(company);
                    Sucess = _producedPartRepository.Save();
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
