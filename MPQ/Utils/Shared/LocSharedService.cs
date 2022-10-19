using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MPQ.Utils.Shared
{
    public class LocSharedService
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        //private readonly IStringLocalizer _localizer;

        //public LocSharedService(IStringLocalizerFactory factory)
        //{
        //    var type = typeof(SharedResource);
        //    var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        //    _localizer = factory.Create("SharedResource", assemblyName.Name);
        //}

        public LocSharedService(IStringLocalizer<SharedResource> localizer)
        {
            this._localizer = localizer;
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            var v = _localizer[key];

            return v;
        }
    }
}
