using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPQ.Helpers;

namespace MPQ.Custom.Middle
{
    public class AutenticacaoCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAutenticacaoHelper _authHelper;

        public AutenticacaoCheckMiddleware(RequestDelegate next, IAutenticacaoHelper authHelper)
        {
            this._next = next;
            this._authHelper = authHelper;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            var cookie = this._authHelper.Get(); //Em requests, por ajax, forçado a renovar cookie

            

            // Move forward into the pipeline
            await _next(httpContext);
        }

    }

    public static class AutenticacaoCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseAutenticacaoCheckMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AutenticacaoCheckMiddleware>();
        }
    }
}
