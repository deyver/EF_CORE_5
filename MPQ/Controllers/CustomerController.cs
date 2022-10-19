using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
using System.Threading.Tasks;

namespace MPQ.Controllers
{
    public class CustomerController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        const string sistema = "Customer";

        #region Propriedades e Construtor

        public CustomerController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            ICustomerRepository customerRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._customerRepository = customerRepository;
            this._mapper = mapper;
        }
        #endregion

        //[Custom.Attribute.CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
        [Route("Customer/GetAllCustomer")]
        public async Task<JsonResult> GetAllCompany()
        {
            var user = await _customerRepository.GetAllAsync();            
            return Json(user);
        }
        #endregion

        #region CRUD

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _customerRepository.Add(new Customer { Id = 0, Name = model.Name });
                    _customerRepository.Save();
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
            var customer = _customerRepository.GetByIdAsync(id).Result;
            return PartialView("_Update", new CustomerViewModel() { Id = customer.Id, Name = customer.Name });
        }

        [HttpPost]
        public ActionResult Update(CustomerViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _customerRepository.Update(new Customer { Id = model.Id, Name = model.Name });
                    _customerRepository.Save();
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
                var company = _customerRepository.GetByIdAsync(id).Result;
                if ((company.Id > 0))
                {
                    _customerRepository.Delete(company);
                    Sucess = _customerRepository.Save();
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