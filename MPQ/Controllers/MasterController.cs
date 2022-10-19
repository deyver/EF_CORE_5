using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using MPQ.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Controllers
{
    public class MasterController : Controller
    {
        public readonly IConfiguration _configuration;
        public readonly IAplicacao _aplicacao;

        public MasterController(IConfiguration configuration, IAplicacao aplicacao)
        {
            _configuration = configuration;
            _aplicacao = aplicacao;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        internal string AddErrors()
        {
            var errors = string.Empty;
            foreach (var modelStateVal in ViewData.ModelState.Values)
            {
                foreach (var item in modelStateVal.Errors.Select(error => error.ErrorMessage))
                {
                    errors += $"<p>{item}</p>";
                }
            }

            return errors;
        }

        internal void AddErrosModel(string mensagem)
        {
            ViewData.ModelState.AddModelError(string.Empty, mensagem);
        }

        internal void AddErrosModel(IEnumerable<string> mensagens)
        {
            foreach (var mensagem in mensagens)
            {
                ViewData.ModelState.AddModelError(string.Empty, mensagem);
            }
        }

        internal void AddErrorsFromModelState()
        {
            var errors = string.Empty;
            foreach (var modelStateVal in ViewData.ModelState.Values)
            {
                foreach (var item in modelStateVal.Errors.Select(error => error.ErrorMessage))
                {
                    ViewData.ModelState.AddModelError(string.Empty, item);
                }
            }
        }

        [HttpPost]
        [Route("[controller]/Exportar")]
        public virtual IActionResult Exportar(IFormCollection arquivo)
        {
            var excelHelper = (IExcelHelper)HttpContext.RequestServices.GetService(typeof(IExcelHelper));
            var content = excelHelper.ExportarXlsx(arquivo["content"]);

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{arquivo["filename"]}.xlsx");
        }

    }
}