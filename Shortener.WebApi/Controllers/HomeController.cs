using Microsoft.AspNetCore.Mvc;

namespace Shortener.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
