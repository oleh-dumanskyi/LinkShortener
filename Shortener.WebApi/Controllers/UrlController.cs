using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Urls.Queries.GetUrlsList;

namespace Shortener.WebApi.Controllers
{
    public class UrlController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<UrlsListVm>> GetAll()
        {
            var query = new GetUrlListQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
