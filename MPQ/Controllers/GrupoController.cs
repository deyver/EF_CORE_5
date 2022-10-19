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
    public class GrupoController : MasterController
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;        
        private readonly IGroupRepository _groupRepository;        
        private readonly IMenuRepository _menuRepository;
        private readonly IGroupMenuRepository _groupMenuRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;
        const string sistema = "Grupo";

        #region Propriedades e Construtor

        public GrupoController(
            IConfiguration configuration, 
            IStringLocalizer<SharedResource> sharedLocalizer,
            IGroupRepository groupRepository,
            IMenuRepository menuRepository,
            IGroupMenuRepository groupMenuRepository,
            IUserGroupRepository userGroupRepository,
            IMapper mapper,
            IAplicacao aplicacao) : base(configuration, aplicacao)
        {
            this._sharedLocalizer = sharedLocalizer;
            this._groupRepository = groupRepository;
            this._menuRepository = menuRepository;
            this._groupMenuRepository = groupMenuRepository;
            this._userGroupRepository = userGroupRepository;
            this._mapper = mapper;
        }
        #endregion

        [CustomAuthorize()]
        public ActionResult Index()
        {
            return View();
        }

        #region Consultas
        
        [Route("Grupo/GetAllGroup")]
        public JsonResult GetAllGroup()
        {
            var _group = _groupRepository.GetAllAsync().Result.Select(f => new { Id = f.Id, Name = f.Name, Description = f.Description });
            return Json(_group);
        }

        #endregion

        #region CRUD

        public void LoadDropDownListMenu()
        {
            try
            {
                List<Menu> menuDb = _menuRepository.GetAllAsync().Result.ToList();

                List<Menu> menuCompleto = new List<Menu>();

                foreach (Menu menuItem in menuDb.Where(m => m.Level == 0).OrderBy(m => m.Sequence))
                {
                    menuCompleto.Add(menuItem);

                    foreach (Menu submenuItem in menuDb.Where(m => m.ParentId == menuItem.Id).OrderBy(m => m.Sequence))
                    {
                        submenuItem.Title = "---" + submenuItem.Title;

                        menuCompleto.Add(submenuItem);
                    }
                }

                var _menu = menuCompleto.Select(s => new SelectListItem { Text = s.Title, Value = s.Id.ToString() }).ToList();
                _menu.Insert(index: 0, item: new SelectListItem(text: "", value: "0"));
                ViewBag.Menu = _menu;
            }
            catch (Exception)
            {
                ViewBag.Menu = new List<SelectListItem>();
            }
        }

        public IActionResult Create()
        {
            LoadDropDownListMenu();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(GrupoViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();

            try
            {
                if (ModelState.IsValid)
                {
                    var _group = new Group { Name = model.Name, Description = model.Description };
                    _groupRepository.Add(_group);
                    Sucess = _groupRepository.Save();

                    _groupMenuRepository.Delete(_groupMenuRepository.GetByGroupIdAsync(_group.Id).Result.ToList());
                    _groupMenuRepository.Save();

                    _groupMenuRepository.Add(model.MenuId.Where(w => w > 0).Select(s => new GroupMenu { GroupId = _group.Id, MenuId = (int)s }).ToList());
                    _groupMenuRepository.Save();

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
            LoadDropDownListMenu();

            var _group = _groupRepository.GetByIdAsync(registro).Result;
            var _menuId = _groupMenuRepository.GetByGroupIdAsync(_group.Id).Result.Select(s => s.MenuId).ToList();
            return PartialView("_Update", new GrupoViewModel() { Id = _group.Id, Name = _group.Name, Description = _group.Description, MenuId = _menuId });
        }

        [HttpPost]
        public ActionResult Update(GrupoViewModel model)
        {
            bool Sucess = false;
            RetornoWS retornoWS = new RetornoWS();
            List<GroupMenu> _groupMenu;

            try
            {
                if (ModelState.IsValid)
                {                    
                    var _group = new Group { Id = model.Id, Name = model.Name, Description = model.Description };
                    _groupRepository.Update(_group);
                    Sucess = _groupRepository.Save();
                    
                    _groupMenu = _groupMenuRepository.GetByGroupIdAsync(model.Id).Result.ToList();
                    if (_groupMenu != null && _groupMenu.Count > 0)
                    {
                        foreach (var groupMenu in _groupMenu)
                        {
                            _groupMenuRepository.Delete(groupMenu);
                            _groupMenuRepository.Save();
                        }
                        //_groupMenuRepository.Delete(_groupMenu);
                        //_groupMenuRepository.Save();
                    }

                    if (model.MenuId != null)
                    {
                        _groupMenuRepository.Add(model.MenuId.Where(w => w > 0).Select(s => new GroupMenu { GroupId = _group.Id, MenuId = (int)s }).ToList());
                        _groupMenuRepository.Save();
                    }

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
                var _group = _groupRepository.GetByIdAsync(registro).Result;
                if ((_group.Id == registro) && (registro != 0))
                {
                    var _userGroup = _userGroupRepository.GetByGroupIdAsync(_group.Id).Result;
                    var _groupMenu = _groupMenuRepository.GetByGroupIdAsync(_group.Id).Result;

                    if (_userGroup.Count > 0) {
                        retornoWS = new RetornoWS { Id = Guid.NewGuid(), Dados = "", Sucesso = false, Mensagem = "Erro na deleção. Existem registros vinculados ao Usuário!", Status = 2 };
                        return Json(retornoWS, new JsonSerializerSettings());
                    }

                    _groupMenuRepository.Delete(_groupMenu);
                    _groupMenuRepository.Save();

                    _groupRepository.Delete(_group);
                    Sucess = _groupRepository.Save();
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