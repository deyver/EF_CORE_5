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
    public class EmpresaController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;
        const string sistema = "Empresa";

        #region Propriedades e Construtor

        public EmpresaController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            ICompanyRepository companyRepository,
            ISiteRepository siteRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._companyRepository = companyRepository;
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
       
        public JsonResult ListaEmpresaAds(string searchField, string searchString, string filtro, string dominio, Guid IdDominio)
        {
            var montaJson = ListaEmpresas(searchField, searchString, filtro, dominio, IdDominio);

            return Json(montaJson);
        }

        [SupportedOSPlatform("windows")]
        private IEnumerable<dynamic> ListaEmpresas(string searchField, string searchString, string filtro, string dominio, Guid IdDominio)
        {
            //var infoDominio = ConsultarDominioPorId(IdDominio).Result;
            var listaRetorno = new List<dynamic>();

            try
            {
                var sortOption = new SortOption("samaccountname", SortDirection.Ascending);
                var directoryEntry = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);

                var diretorioDominio = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
                var dadosDominio = new DirectorySearcher(diretorioDominio,
                    "(&(objectCategory=person)(objectClass=user)(" + searchField + "=" + searchString + filtro +
                    "))", new[] { "samaccountname" })
                {
                    Sort = sortOption,
                    SizeLimit = 100
                };

                var valores = dadosDominio.FindAll();
                foreach (var entry in from SearchResult sr in valores select sr.GetDirectoryEntry() into entry where entry.Properties.Contains("displayname") where entry.Properties["displayname"].Count > 0 select entry)
                {
                    listaRetorno.Add(new
                    {
                        value = entry.Properties["samaccountname"][0].ToString(),
                        label = entry.Properties["samaccountname"][0] + " - " + entry.Properties["displayname"][0]
                    });
                }
            }
            catch (Exception e)
            {
                listaRetorno.Add(new { value = string.Empty, label = "Erro" });
            }

            return listaRetorno;
        }

        [Route("Empresa/GetAllCompany")]
        public async Task<JsonResult> GetAllCompany(string dominio, string usuario, Guid IdDominio)
        {
            var user = await _companyRepository.GetAllAsync();            
            return Json(user);
        }


        [SupportedOSPlatform("windows")]
        public JsonResult ConsultaNomeUsuarioPorMatricula(string dominio, string usuario, Guid IdDominio)
        {
            if (dominio == null || usuario == null)
            {
                return Json(new List<dynamic>() { new { value = "" } });
            }

            //var infoDominio = ConsultarDominioPorId(IdDominio).Result;
            var listaRetorno = new List<dynamic>();

            var sortOption = new SortOption("samaccountname", SortDirection.Ascending);
            var directoryEntry = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var strDomain = directoryEntry.Properties["defaultNamingContext"].Value.ToString();

            var diretorioDominio = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var dadosDominio = new DirectorySearcher(diretorioDominio,
            $"(&(objectCategory=person)(objectClass=user)(sAMAccountName={usuario}))", new[] { "samaccountname" })
            {
                Sort = sortOption,
                SizeLimit = 100
            };

            var valores = dadosDominio.FindAll();
            foreach (var entry in from SearchResult sr in valores select sr.GetDirectoryEntry() into entry where entry.Properties.Contains("displayname") where entry.Properties["displayname"].Count > 0 select entry)
            {
                listaRetorno.Add(new
                {
                    value = entry.Properties["displayname"][0]
                });
            }

            return Json(listaRetorno);
        }

        [SupportedOSPlatform("windows")]
        private bool ValidarExisteUsuarioAD(string usuario)
        {
            var sortOption = new SortOption("samaccountname", SortDirection.Ascending);
            var directoryEntry = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var strDomain = directoryEntry.Properties["defaultNamingContext"].Value.ToString();

            var de = new DirectoryEntry(this._configuration.GetSection("LDAP").GetSection("Instance").Value);
            var dm = new DirectorySearcher(de,
                $"(&(objectCategory=person)(objectClass=user)(sAMAccountName={usuario}))", new[] { "samaccountname" })
            {
                Sort = sortOption,
                SizeLimit = 100
            };

            var valores = dm.FindAll();

            return valores.Count > 0;
        }

        #endregion

        #region CRUD

        public void LoadDropDownListSite()
        {
            try
            {
                //var opcao = this._sharedLocalizer["SelectOpcao"];
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
                //var opcao = this._sharedLocalizer["SelectOpcao"];
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
            //LoadDropDownListSite();
            //LoadDropDownListStatus();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(EmpresaViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _companyRepository.Add(new Company { Id = 0, Name = model.Name });
                    _companyRepository.Save();
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
            var user = _companyRepository.GetByIdAsync(registro).Result;
            return PartialView("_Update", new EmpresaViewModel() { Id = user.Id, Name = user.Name });
        }

        [HttpPost]
        public ActionResult Update(EmpresaViewModel model)
        {
            RetornoWS retornoWS;

            try
            {
                if (ModelState.IsValid)
                {
                    _companyRepository.Update(new Company { Id = model.Id, Name = model.Name });
                    _companyRepository.Save();
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
                var company = _companyRepository.GetByIdAsync(registro).Result;
                if ((company.Id > 0))
                {
                    if (company.Sites.Count() > 0) // SÓ DELETA EMPRESAS SEM VÍNCULO COM SITES (FK)
                    {
                        retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados ao Site!", Status = 2 };
                        return Json(retornoWS, new JsonSerializerSettings());
                    }
                    _companyRepository.Delete(company);
                    Sucess = _companyRepository.Save();
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