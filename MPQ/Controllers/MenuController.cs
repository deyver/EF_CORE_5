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
    public class MenuController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;        
        private readonly IMenuRepository _menuRepository;
        private readonly IGroupMenuRepository _groupMenuRepository;
        private readonly IMapper _mapper;
        const string sistema = "Grupo";

        #region Propriedades e Construtor

        public MenuController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMenuRepository menuRepository,
            IGroupMenuRepository groupMenuRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._menuRepository = menuRepository;
            this._groupMenuRepository = groupMenuRepository;
            this._mapper = mapper;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
        
        [Route("Menu/GetAllMenu")]
        public JsonResult GetAllMenu()
        {
            var _menu = _menuRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Level = f.Level, Sequence = f.Sequence, parentId = GetParent(f.ParentId), Name = f.Name, Title = f.Title, Url = f.Url, IconUrl = f.IconUrl });
            return Json(_menu);
        }

        private string GetParent(int? parentId) {
            return (parentId == null || parentId == 0 ? "" : _menuRepository.GetByIdAsync(Convert.ToInt32(parentId)).Result.Name);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListParent()
        {
            try
            {
                var _menu = _menuRepository.GetAllAsync().Result.ToList().Where(f => f.ParentId == null || f.ParentId == 0).OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                _menu.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Pai = _menu;
            }
            catch (Exception)
            {
                ViewBag.Menu = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListParent();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(MenuViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _menu = new Menu { Id = model.Id, Level = model.Level, Sequence = model.Sequence, ParentId = model.ParentId, Name = model.Name, Title = model.Title, Url = model.Url, IconUrl = model.IconUrl };
                    _menuRepository.Add(_menu);
                    Sucess = _menuRepository.Save();

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

        public ActionResult Update(int registro, string dominio)
        {
            LoadDropDownListParent();

            var _menu = _menuRepository.GetByIdAsync(registro).Result;
            return PartialView("_Update", new MenuViewModel() { Id = _menu.Id, Level = _menu.Level, Sequence = _menu.Sequence, ParentId = _menu.ParentId, Name = _menu.Name, Title = _menu.Title, Url = _menu.Url, IconUrl = _menu.IconUrl });
        }

        [HttpPost]
        public ActionResult Update(MenuViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _menu = new Menu { Id = model.Id, Level = model.Level, Sequence = model.Sequence, ParentId = model.ParentId, Name = model.Name, Title = model.Title, Url = model.Url, IconUrl = model.IconUrl };
                    _menuRepository.Update(_menu);
                    Sucess = _menuRepository.Save();

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
                var _menu = _menuRepository.GetByIdAsync(registro).Result;
                if ((_menu.Id == registro) && (registro != 0))
                {
                    var _groupMenu = _groupMenuRepository.GetByMenuIdAsync(_menu.Id).Result;

                    if (_groupMenu.Count > 0) {
                        retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados ao Grupo!", Status = 2 };
                        return Json(retornoWS, new JsonSerializerSettings());
                    }

                    _menuRepository.Delete(_menu);
                    Sucess = _menuRepository.Save();
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