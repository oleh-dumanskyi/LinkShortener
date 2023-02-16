using Microsoft.AspNetCore.Mvc;

namespace Shortener.WebApi.Controllers
{
    [Route("[controller]/")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
