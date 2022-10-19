using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using MPQ.Utils.Shared;
using System;
using System.Collections.Generic;

namespace MPQ.Helpers
{
    public class UIHelper : IUIHelper
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UIHelper(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS, Func<T, object> text, Func<T, object> value)
        {
            var opcao = _sharedLocalizer["SelecOpcao"];

            return GerarDropDownList(retornoWS, opcao, text, value);
        }

        public List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS, string primeiroItem, Func<T, object> text, Func<T, object> value)
        {
            var lista = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = primeiroItem,
                    Value = "-99",
                }
            };

            foreach (T item in retornoWS)
            {
                lista.Add(new SelectListItem()
                {
                    Value = value(item).ToString(),
                    Text = text(item).ToString()
                });
            }

            return lista;
        }

        public List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS, string primeiroItem)
        {
            return GerarDropDownList(retornoWS, primeiroItem, o => o, o => o);
        }

        public List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS)
        {
            return GerarDropDownList(retornoWS, _sharedLocalizer["SelecOpcao"]);
        }
    }
}
