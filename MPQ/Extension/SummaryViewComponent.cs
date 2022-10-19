using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MPQ.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return View();
        }
    }
}
