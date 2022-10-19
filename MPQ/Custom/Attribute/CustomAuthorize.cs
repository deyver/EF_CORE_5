using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPQ.Custom.Filters;

namespace MPQ.Custom.Attribute
{
    public class CustomAuthorize : TypeFilterAttribute
    {
        public CustomAuthorize() : base(typeof(CustomAuthorizeFilter))
        {
        }
    }
}
