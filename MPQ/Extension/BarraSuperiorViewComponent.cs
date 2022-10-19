using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPQ.Data.Repositories;
using MPQ.Models;
using MPQ.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Extensions
{
    public class BarraSuperiorViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _context;
        private readonly ISiteRepository _siteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserSiteRepository _userSiteRepository;

        public BarraSuperiorViewComponent(
            IHttpContextAccessor context,
            ISiteRepository siteRepository,
            IUserRepository userRepository,
            IUserSiteRepository userSiteRepository) 
        {
            this._context = context;
            this._siteRepository = siteRepository;
            this._userRepository = userRepository;
            this._userSiteRepository = userSiteRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cookieValue = StringUtils.Base64Decode(Request.Cookies["mpq_login_session"] ?? "").Split('\\');
            var model = new BarraSuperiorViewModel { _userId = Convert.ToInt32(cookieValue[6]), _siteId = Convert.ToInt32(cookieValue[2].Split('|')[0]), _Language = cookieValue[5] };

            LoadDropDownListIdioma();
            LoadDropDownListSite(model._userId);
            
            return View(model);
        }

        public void LoadDropDownListIdioma()
        {
            try
            {
                List<SelectListItem> idioma = new List<SelectListItem>();
                idioma.Insert(index: 0, item: new SelectListItem(text: "Português (Brasil)", value: "pt-BR"));
                idioma.Insert(index: 1, item: new SelectListItem(text: "Espanhol", value: "es"));
                ViewBag.Idioma = idioma;
            }
            catch (Exception)
            {
                ViewBag.Idioma = new List<SelectListItem>();
            }
        }

        public void LoadDropDownListSite(int _userId)
        {
            try
            {
                var _userSite = _userSiteRepository.GetByUserIdAsync(_userId).Result.Select(s => new { item = s.SiteId }).ToList();
                var _site = _siteRepository.GetAllAsync().Result.Where(f => _userSite.Any(a => a.item == f.Id)).ToList().OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
                ViewBag.Site = _site;
            }
            catch (Exception)
            {
                ViewBag.Site = new List<SelectListItem>();
            }
        }

    }
}