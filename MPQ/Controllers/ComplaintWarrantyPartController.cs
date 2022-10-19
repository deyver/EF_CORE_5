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
    public class ComplaintWarrantyPartController : MasterController
    {
        private readonly IHttpContextAccessor _context;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IComplaintWarrantyPartRepository _complaintWarrantyPartRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBusinessUnitRepository _businessUnitRepository;
        private readonly IUserSiteRepository _userSiteRepository;
        const string sistema = "Apontamento de Reclamação na Garantia";

        #region Propriedades e Construtor

        public ComplaintWarrantyPartController(
            IConfiguration configuration,
            IHttpContextAccessor context,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IComplaintWarrantyPartRepository complaintWarrantyPartRepository,
            ISiteRepository siteRepository,
            ICustomerRepository customerRepository,
            IBusinessUnitRepository businessUnitRepository,
            IUserSiteRepository userSiteRepository,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._context = context;
            this._sharedLocalizer = sharedLocalizer;
            this._complaintWarrantyPartRepository = complaintWarrantyPartRepository;
            this._siteRepository = siteRepository;
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
       
        [Route("ComplaintWarrantyPart/GetAllComplaintWarrantyPart")]
        public JsonResult GetAllComplaintWarrantyPart()
        {
            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var _siteId = Convert.ToInt32(cookieValue[2].Split('|')[0]);

            var _complaintWarrantyPart = _complaintWarrantyPartRepository.GetBySiteIdAsync(_siteId).Result.Select(f => new { Id = f.Id, Site = f.Site.Name, Customer = f.Customer.Name, ReceiptDate = string.Format("{0:dd/MM/yyyy}", f.ReceiptDate), PartQuantity = f.PartQuantity, 
                                                                                                                             Issue = f.Issue.Substring(0, f.Issue.Length >= 50 ? 50 : f.Issue.Length) + "...", Legitimate = f.Legitimate, Status = f.Status, BusinessUnit = f.BusinessUnit.Name });
            return Json(_complaintWarrantyPart);
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

        public void LoadDropDownListStatus()
        {
            try
            {
                List<SelectListItem> _status = new List<SelectListItem>() { new SelectListItem { Text = "Aberto", Value = "Aberto" }, new SelectListItem { Text = "Em andamento", Value = "Em andamento" }, new SelectListItem { Text = "Fechado", Value = "Fechado" } };
                ViewBag.Status = _status;
            }
            catch (Exception)
            {
                ViewBag.Status = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListLegitimates()
        {
            try
            {
                List<SelectListItem> _legitimate = new List<SelectListItem>() { new SelectListItem { Text = "", Value = "" }, new SelectListItem { Text = "SIM", Value = "true" }, new SelectListItem { Text = "NÃO", Value = "false" } };
                ViewBag.Legitimate = _legitimate;
            }
            catch (Exception)
            {
                ViewBag.Legitimate = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListSites();
            LoadDropDownListCustomers();
            LoadDropDownListBusinessUnits();
            LoadDropDownListStatus();
            LoadDropDownListLegitimates();

            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var _siteId = Convert.ToInt32(cookieValue[2].Split('|')[0]);

            return PartialView("_Create", new ComplaintWarrantyPartViewModel() { SiteId = _siteId, ReceiptDate = DateTime.Now });
        }

        [HttpPost]
        public IActionResult Create(ComplaintWarrantyPartViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    _complaintWarrantyPartRepository.Add(new ComplaintWarrantyPart { SiteId = model.SiteId, CustomerId = model.CustomerId, ReceiptDate = model.ReceiptDate, PartQuantity = model.PartQuantity, Issue = model.Issue,  Legitimate = model.Legitimate, Status = model.Status, BusinessUnitId = model.BusinessUnitId });
                    Sucess = _complaintWarrantyPartRepository.Save();

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
            LoadDropDownListBusinessUnits();
            LoadDropDownListStatus();
            LoadDropDownListLegitimates();

            var _complaintWarrantyPart = _complaintWarrantyPartRepository.GetByIdAsync(registro).Result;            
            return PartialView("_Update", new ComplaintWarrantyPartViewModel() {    Id = _complaintWarrantyPart.Id, SiteId = _complaintWarrantyPart.SiteId, CustomerId = _complaintWarrantyPart.CustomerId, ReceiptDate = _complaintWarrantyPart.ReceiptDate, PartQuantity = _complaintWarrantyPart.PartQuantity, Issue = _complaintWarrantyPart.Issue, 
                                                                                    Legitimate = _complaintWarrantyPart.Legitimate, Status = _complaintWarrantyPart.Status, BusinessUnitId = _complaintWarrantyPart.BusinessUnitId });
        }

        [HttpPost]
        public ActionResult Update(ComplaintWarrantyPartViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _complaintWarrantyPart = new ComplaintWarrantyPart { Id = model.Id, SiteId = model.SiteId, CustomerId = model.CustomerId, ReceiptDate = model.ReceiptDate, PartQuantity = model.PartQuantity, Issue = model.Issue, Legitimate = model.Legitimate, Status = model.Status, BusinessUnitId = model.BusinessUnitId };
                    _complaintWarrantyPartRepository.Update(_complaintWarrantyPart); 
                    Sucess = _complaintWarrantyPartRepository.Save();

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
                var _complaint = _complaintWarrantyPartRepository.GetByIdAsync(registro).Result;
                if ((_complaint.Id == registro) && (registro != 0)) 
                {
                    _complaintWarrantyPartRepository.Delete(_complaint);
                    Sucess = _complaintWarrantyPartRepository.Save();
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