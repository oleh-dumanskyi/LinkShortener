using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Application.Urls.Commands.DeleteUrl;
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
        [Authorize]
        public async Task<ActionResult<UrlsListVm>> GetAll()
        {
            var query = new GetUrlListQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            ViewBag.vm = vm;
            return View("GetAll");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateUrlDto createUrlDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidDataException();
                var command = _mapper.Map<CreateUrlCommand>(createUrlDto);
                command.UserId = UserId;
                var noteId = await Mediator.Send(command);
                return RedirectToAction("GetAll");
            }
            catch (InvalidDataException)
            {
                ViewBag.InputError = "Некоректні дані";
                return View();
            }
            
        }

        [Route("~/[controller]/{ShortenedUriPart}")]
        [HttpGet("{ShortenedUriPart}")]
        public async Task<RedirectResult> Redirect(string ShortenedUriPart)
        {
            var query = new GetUrlQuery
            {
                UriShortenedPart = ShortenedUriPart
            };
            var result = await Mediator.Send(query);
            return new RedirectResult(result.BaseUri.AbsoluteUri);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Delete([FromForm]ModifyUrlDto modifyUrlDto)
        {
            try
            {
                var command = _mapper.Map<DeleteUrlCommand>(modifyUrlDto);
                command.UserId = UserId;
                Mediator.Send(command);
            }
            catch (NotFoundException)
            {
                return RedirectToAction("GetAll");
            }
            return RedirectToAction("GetAll");
        }
    }
}
