using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Application.Urls.Queries.GetUrlByShortUri;
using Shortener.Application.Urls.Queries.GetUrlsList;
using Shortener.WebApi.Models;

namespace Shortener.WebApi.Controllers
{
    public class UrlController : BaseController
    {
        private readonly IMapper _mapper;
        public UrlController(IMapper mapper) => _mapper = mapper;

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

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUrlDto createUrlDto)
        {
            var command = _mapper.Map<CreateUrlCommand>(createUrlDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        [Route("~/[controller]/{ShortenedUriPart}")]
        [HttpGet("{ShortenedUriPart}")]
        public async Task<RedirectResult> Redirect(string ShortenedUriPart)
        {
            var query = new GetUrlQuery()
            {
                UserId = UserId,
                UriShortenedPart = ShortenedUriPart
            };
            var result = await Mediator.Send(query);
            return new RedirectResult(result.BaseUri.AbsoluteUri);
        }
    }
}
