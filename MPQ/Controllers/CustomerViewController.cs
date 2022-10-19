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
using MPQ.Utils;
using Microsoft.AspNetCore.Http;

namespace MPQ.Controllers
{
    public class CustomerViewController : MasterController
    {
        private readonly IHttpContextAccessor _context;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ICustomerViewRepository _customerViewRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IIndicatorRepository _indicatorRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBusinessUnitRepository _businessUnitRepository;
        private readonly IUserSiteRepository _userSiteRepository;
        const string sistema = "Apontamento de Reclamação";

        #region Propriedades e Construtor

        public CustomerViewController(
            IConfiguration configuration,
            IHttpContextAccessor context,
            IStringLocalizer<SharedResource> sharedLocalizer,
            ICustomerViewRepository customerViewRepository,
            ISiteRepository siteRepository,
            IIndicatorRepository indicatorRepository,
            ICustomerRepository customerRepository,
            IBusinessUnitRepository businessUnitRepository,
            IUserSiteRepository userSiteRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._context = context;
            this._sharedLocalizer = sharedLocalizer;
            this._customerViewRepository = customerViewRepository;
            this._siteRepository = siteRepository;
            this._indicatorRepository = indicatorRepository;
            this._customerRepository = customerRepository;
            this._businessUnitRepository = businessUnitRepository;
            this._userSiteRepository = userSiteRepository;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
       
        [Route("CustomerView/GetAllCustomerView")]
        public JsonResult GetAllCustomerView()
        {
            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var _siteId = Convert.ToInt32(cookieValue[2].Split('|')[0]);

            var _customerView = _customerViewRepository.GetBySiteIdAsync(_siteId).Result.Select(f => new { Id = f.Id, Site = f.Site.Name, BusinessUnit = f.BusinessUnit.Name, Indicator = f.Indicator.Name, Customer = f.Customer.Name, Year = f.Year, WeekNumber = f.WeekNumber, Result = f.Result });
            return Json(_customerView);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListSites()
        {
            try
            {
                var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
                var _userId = Convert.ToInt32(cookieValue[6]);
                var _userSite = _userSiteRepository.GetByUserIdAsync(_userId).Result.Select(s => new { item = s.SiteId }).ToList();
                var _site = _siteRepository.GetAllAsync().Result.Where(f => _userSite.Any(a => a.item == f.Id)).ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();

                _site.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Site = _site;
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
            LoadDropDownListCustomers();
            LoadDropDownListBusinessUnits();

            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var _siteId = Convert.ToInt32(cookieValue[2].Split('|')[0]);

            return PartialView("_Create", new CustomerViewViewModel() { SiteId = _siteId });
        }

        [HttpPost]
        public IActionResult Create(CustomerViewViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _customerViewRepository.Add(new CustomerView { SiteId = model.SiteId, BusinessUnitId = model.BusinessUnitId, IndicatorId = model.IndicatorId, CustomerId = model.CustomerId, Year = model.Year, WeekNumber = model.WeekNumber, Result = model.Result });
                    Sucess = _customerViewRepository.Save();

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
            LoadDropDownListCustomers();
            LoadDropDownListBusinessUnits();

            var _customerView = _customerViewRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new CustomerViewViewModel() { Id = _customerView.Id, SiteId = _customerView.SiteId, BusinessUnitId = _customerView.BusinessUnitId, IndicatorId = _customerView.IndicatorId, CustomerId = _customerView.CustomerId, Year = _customerView.Year, WeekNumber = _customerView.WeekNumber, Result = _customerView.Result });
        }

        [HttpPost]
        public ActionResult Update(CustomerViewViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _customerView = new CustomerView { Id = model.Id, SiteId = model.SiteId, BusinessUnitId = model.BusinessUnitId, IndicatorId = model.IndicatorId, CustomerId = model.CustomerId, Year = model.Year, WeekNumber = model.WeekNumber, Result = model.Result };
                    _customerViewRepository.Update(_customerView); 
                    Sucess = _customerViewRepository.Save();

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
                var _customerView = _customerViewRepository.GetByIdAsync(registro).Result;
                if ((_customerView.Id == registro) && (registro != 0)) 
                {
                    _customerViewRepository.Delete(_customerView);
                    Sucess = _customerViewRepository.Save();
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