using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MPQ.Helpers;
using MPQ.Models;
//using MPQ.Services.Interfaces;
using MPQ.Utils.Cache;
using MPQ.Data.Repositories;
using MPQ.Utils;
using MPQ.Domain;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace YMSManager.Extensions
{
    public class MenuViewComponent : ViewComponent
    {
        public static string KeyMenu { get => "sessionMenu"; }


        private readonly IHttpContextAccessor _context;
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IGroupMenuRepository _groupMenuRepository;
        private readonly IMenuRepository _menuRepository;

        //private readonly IPermissaoServices _permissaoServices;
        //private readonly IPermissaoUsuarioService _permissaoUsuario;
        private readonly ISessionHelper _sessionHelper;
        private readonly IAplicacao _aplicacao;

        public MenuViewComponent(
            //IPermissaoServices permissaoServices,
            //IPermissaoUsuarioService permissaoUsuario,
            IHttpContextAccessor context,
            IUserRepository userRepository,
            IUserGroupRepository userGroupRepository,
            IGroupMenuRepository groupMenuRepository,
            IMenuRepository menuRepository,
            ISessionHelper sessionHelper, 
            IAplicacao aplicacao)
        {
            //this._permissaoServices = permissaoServices;
            //this._permissaoUsuario = permissaoUsuario;
            this._sessionHelper = sessionHelper;
            this._aplicacao = aplicacao;
            this._context = context;
            this._userRepository = userRepository;
            this._userGroupRepository = userGroupRepository;
            this._groupMenuRepository = groupMenuRepository;
            this._menuRepository = menuRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new List<MenuViewModel>();
            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var login = cookieValue[1];
            var routeValues = _context.HttpContext.Request.RouteValues;
            var actionName = (routeValues.ContainsKey("action") ? routeValues["action"].ToString().ToLower().Trim() : "");
            var controllerName = (routeValues.ContainsKey("controller") ? routeValues["controller"].ToString().ToLower().Trim() : "");
            var _userId = _userRepository.GetByLoginAsync(login).Result.Id;
            var _groupId = _userGroupRepository.GetByUserIdAsync(_userId).Result.Select(s => new { item = s.GroupId }).ToList();
            var _menuId = _groupMenuRepository.GetAllAsync().Result.Where(f => _groupId.Any(a => a.item == f.GroupId)).Select(s => new { item = s.MenuId }).ToList();
            var _menu = _menuRepository.GetAllAsync().Result.Where(f => _menuId.Any(a => a.item == f.Id));

            if (login != "") {
                foreach (Menu item in _menu)
                {
                    model.Add(new MenuViewModel() { Id = item.Id, Level = item.Level, Sequence = item.Sequence, ParentId = item.ParentId, Name = item.Name, Title = item.Title, Url = item.Url, IconUrl = item.IconUrl });
                }
            }

            return View(model);
        }
    }
}
