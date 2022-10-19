using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPQ.Utils.Permissao;
using MPQ.Utils;
using MPQ.Helpers;
using MPQ.Data.Repositories;

namespace MPQ.Custom.Filters
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IGroupMenuRepository _groupMenuRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IPermissaoHelper _permissaoHelper;
        private readonly IAutenticacaoHelper _authHelper;

        public CustomAuthorizeFilter(
            IUserRepository userRepository,
            IUserGroupRepository userGroupRepository,
            IGroupMenuRepository groupMenuRepository,
            IMenuRepository menuRepository,
            IPermissaoHelper permissaoHelper, 
            IAutenticacaoHelper authHelper)
        {
            this._permissaoHelper = permissaoHelper;
            ///this._permissaoService = permissaoService;
            this._authHelper = authHelper;
            this._userRepository = userRepository;
            this._userGroupRepository = userGroupRepository;
            this._groupMenuRepository = groupMenuRepository;
            this._menuRepository = menuRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var cookie = this._authHelper.Get();

            if(cookie == null) //Usuário não está logado.
            {
                this.NaoAutenticado(context);
                return;
            }

            var cookieValue = StringUtils.Base64Decode(cookie).Split('\\');
            var login = cookieValue[1];
            var routeValues = context.HttpContext.Request.RouteValues;            
            var actionName = (routeValues.ContainsKey("action") ? routeValues["action"].ToString().ToLower().Trim() : "");
            var controllerName = (routeValues.ContainsKey("controller") ? routeValues["controller"].ToString().ToLower().Trim() : "");
            var _userId = _userRepository.GetByLoginAsync(login).Result.Id;
            var _groupId = _userGroupRepository.GetByUserIdAsync(_userId).Result.Select(s => new { item = s.GroupId }).ToList();
            var _menuId = _groupMenuRepository.GetAllAsync().Result.Where(f => _groupId.Any(a => a.item == f.GroupId)).Select(s => new { item = s.MenuId }).ToList();
            var _controller = _menuRepository.GetAllAsync().Result.Where(f => f.Url?.ToString().ToLower().Trim() == controllerName && _menuId.Any(a => a.item == f.Id)).Select(s => new { s.Id }).ToList();
            var possuiPermissao = _controller.Count > 0;

            if (possuiPermissao == false)
            {
                this.NaoAuthorizado(context);
                return;
            }
        }

        void NaoAutenticado(AuthorizationFilterContext context)
        {
            context.Result = new RedirectToActionResult(actionName: "Index", controllerName: "Login", routeValues: new { });
        }

        void NaoAuthorizado(AuthorizationFilterContext context)
        {
            context.Result = new RedirectToActionResult(actionName: "Index", controllerName: "Home",  routeValues: new { msg = "401" });
        }
    }
}