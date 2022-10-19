using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace MPQ.Helpers
{
    public interface IUIHelper
    {
        List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS, Func<T, object> text, Func<T, object> value);
        List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS, string primeiroItem, Func<T, object> text, Func<T, object> value);
        List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS, string primeiroItem);
        List<SelectListItem> GerarDropDownList<T>(IEnumerable<T> retornoWS);
    }
}