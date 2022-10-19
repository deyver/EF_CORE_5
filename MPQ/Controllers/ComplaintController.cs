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
    public class ComplaintController : MasterController
    {
        private readonly IHttpContextAccessor _context;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IComplaintRepository _complaintRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IIndicatorRepository _indicatorRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBusinessUnitRepository _businessUnitRepository;
        private readonly IUserSiteRepository _userSiteRepository;
        const string sistema = "Apontamento de Reclamação";

        #region Propriedades e Construtor

        public ComplaintController(
            IConfiguration configuration,
            IHttpContextAccessor context,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IComplaintRepository complaintRepository,
            ISiteRepository siteRepository,
            IIndicatorRepository indicatorRepository,
            ICustomerRepository customerRepository,
            IBusinessUnitRepository businessUnitRepository,
            IUserSiteRepository userSiteRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._context = context;
            this._sharedLocalizer = sharedLocalizer;
            this._complaintRepository = complaintRepository;
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
       
        [Route("Complaint/GetAllComplaint")]
        public JsonResult GetAllComplaint()
        {
            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var _siteId = Convert.ToInt32(cookieValue[2].Split('|')[0]);

            var _complaint = _complaintRepository.GetBySiteIdAsync(_siteId).Result.Select(f => new {    Id = f.Id, Indicator = f.Indicator.Name, ComplaintDate = string.Format("{0:dd/MM/yyyy}", f.ComplaintDate), Customer = f.Customer.Name, Quantity = f.Quantity, 
                                                                                                        Issue = f.Issue.Substring(0, f.Issue.Length >= 50 ? 50 : f.Issue.Length) + "...", 
                                                                                                        ContainmentAction = f.ContainmentAction.Substring(0, f.ContainmentAction.Length >= 50 ? 50 : f.ContainmentAction.Length) + "...", 
                                                                                                        ActionDate = string.Format("{0:dd/MM/yyyy}", f.ActionDate), ActionResponsible = f.ActionResponsible, GqrsNumber = f.GqrsNumber, 
                                                                                                        SummarySent = f.SummarySent, Site = f.Site.Name, BusinessUnit = f.BusinessUnit.Name });
            return Json(_complaint);
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

        public void LoadDropDownListSumarys()
        {
            try
            {
                List<SelectListItem> _sumary = new List<SelectListItem>() { new SelectListItem { Text = "N/A", Value = "N/A" }, new SelectListItem { Text = "SIM", Value = "SIM" }, new SelectListItem { Text = "NÃO", Value = "NÃO" } };
                ViewBag.Sumary = _sumary;
            }
            catch (Exception)
            {
                ViewBag.Sumary = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSites();
            LoadDropDownListIndicators();
            LoadDropDownListCustomers();
            LoadDropDownListBusinessUnits();
            LoadDropDownListSumarys();

            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var _siteId = Convert.ToInt32(cookieValue[2].Split('|')[0]);

            return PartialView("_Create", new ComplaintViewModel() { SiteId = _siteId, ActionDate = DateTime.Now, ComplaintDate = DateTime.Now });
        }

        [HttpPost]
        public IActionResult Create(ComplaintViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _complaintRepository.Add(new Complaint {    IndicatorId = model.IndicatorId, ComplaintDate = model.ComplaintDate, CustomerId = model.CustomerId, Quantity = model.Quantity, Issue = model.Issue, ContainmentAction = model.ContainmentAction, ActionDate = model.ActionDate, 
                                                                ActionResponsible = model.ActionResponsible, GqrsNumber = model.GqrsNumber, SummarySent = model.SummarySent, SiteId = model.SiteId, BusinessUnitId = model.BusinessUnitId });
                    Sucess = _complaintRepository.Save();

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
            LoadDropDownListSumarys();

            var _complaint = _complaintRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new ComplaintViewModel() {    Id = _complaint.Id, IndicatorId = _complaint.IndicatorId, ComplaintDate = _complaint.ComplaintDate, CustomerId = _complaint.CustomerId, Quantity = _complaint.Quantity, Issue = _complaint.Issue, ContainmentAction = _complaint.ContainmentAction,
                                                                        ActionDate = _complaint.ActionDate, ActionResponsible = _complaint.ActionResponsible, GqrsNumber = _complaint.GqrsNumber, SummarySent = _complaint.SummarySent, SiteId = _complaint.SiteId, BusinessUnitId = _complaint.BusinessUnitId });
        }

        [HttpPost]
        public ActionResult Update(ComplaintViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _complaint = new Complaint {    Id = model.Id, IndicatorId = model.IndicatorId, ComplaintDate = model.ComplaintDate, CustomerId = model.CustomerId, Quantity = model.Quantity, Issue = model.Issue, ContainmentAction = model.ContainmentAction, ActionDate = model.ActionDate,
                                                        ActionResponsible = model.ActionResponsible, GqrsNumber = model.GqrsNumber, SummarySent = model.SummarySent, SiteId = model.SiteId, BusinessUnitId = model.BusinessUnitId };
                    _complaintRepository.Update(_complaint); 
                    Sucess = _complaintRepository.Save();

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
                var _complaint = _complaintRepository.GetByIdAsync(registro).Result;
                if ((_complaint.Id == registro) && (registro != 0)) 
                {
                    _complaintRepository.Delete(_complaint);
                    Sucess = _complaintRepository.Save();
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