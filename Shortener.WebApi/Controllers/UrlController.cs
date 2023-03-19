using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Common.Exceptions;
using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Application.Urls.Commands.DeleteUrl;
using Shortener.Application.Urls.Commands.EditUrl;
using Shortener.Application.Urls.Queries.GetDetails;
using Shortener.Application.Urls.Queries.GetUrlByShortUri;
using Shortener.Application.Urls.Queries.GetUrlsList;
using Shortener.WebApi.Models;
using System.Data;
using System.Net;
using Shortener.Application.Redirections.Commands.Create;
using Shortener.Application.Redirections.Queries.GetStatistics;

namespace Shortener.WebApi.Controllers
{
    public class UrlController : BaseController
    {
        private readonly IMapper _mapper;

        public UrlController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UrlsListVm>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetUrlListQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query, cancellationToken);
            ViewBag.vm = vm;
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
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
        public async Task<ActionResult<Guid>> Create([FromForm] CreateUrlDto createUrlDto,
            CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidDataException();
                var command = _mapper.Map<CreateUrlCommand>(createUrlDto);
                command.UserId = UserId;
                var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
                command.CurrentUri = location;
                var noteId = await Mediator.Send(command, cancellationToken);
                return RedirectToAction("GetAll");
            }
            catch (InvalidDataException)
            {
                ViewBag.InputError = "Некоректні дані";
                return View();
            }

        }

        [Route("~/{ShortenedUriPart}")]
        [HttpGet("{ShortenedUriPart}")]
        public async Task<RedirectResult> Redirect(string ShortenedUriPart, CancellationToken cancellationToken)
        {
            var urlQuery = new GetUrlQuery
            {
                UriShortenedPart = ShortenedUriPart
            };
            var result = await Mediator.Send(urlQuery, cancellationToken);
            var redirectCommand = new CreateRedirectionCommand
            {
                UserAgent = Request.Headers["User-Agent"].ToString(),
                IpAddress = IPAddress.Parse("158.24.24.151"),
                Token = _configuration["IpstackApiToken"],
                UrlId = result.Id
            };
            await Mediator.Send(redirectCommand, cancellationToken);
            return new RedirectResult(result.BaseUri.AbsoluteUri);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Delete([FromForm] ModifyUrlDto modifyUrlDto,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = _mapper.Map<DeleteUrlCommand>(modifyUrlDto);
                command.UserId = UserId;
                Mediator.Send(command, cancellationToken);
            }
            catch (NotFoundException)
            {
                return RedirectToAction("GetAll");
            }
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetDetails()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> GetDetails([FromForm] ModifyUrlDto urlDto,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = _mapper.Map<GetUrlDetailsQuery>(urlDto);
                command.UserId = UserId;
                var result = await Mediator.Send(command, cancellationToken);
                ViewBag.UrlDetails = result;
                ViewBag.Id = result.Id;
                return View();
            }
            catch (NotFoundException)
            {
                return RedirectToAction("GetAll");
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EditView([FromForm] ModifyUrlDto urlDto,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = _mapper.Map<GetUrlDetailsQuery>(urlDto);
                command.UserId = UserId;
                var result = await Mediator.Send(command, cancellationToken);
                ViewBag.UrlDetails = result;
                return View();
            }
            catch (NotFoundException)
            {
                return RedirectToAction("GetAll");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Edit([FromForm] EditUrlCommand editCommand,
            CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidDataException();
                editCommand.UserId = UserId;
                await Mediator.Send(editCommand, cancellationToken);
                return RedirectToAction("GetAll");
            }
            catch (NotFoundException)
            {
                TempData["ErrorMessage"] = "Помилка редагування! Некоректні дані!";
                return RedirectToAction("GetAll");
            }
            catch (InvalidDataException)
            {
                TempData["ErrorMessage"] = "Помилка редагування! Некоректні дані!";
                return RedirectToAction("GetAll");
            }
            catch (ConstraintException)
            {
                TempData["ErrorMessage"] = "Помилка редагування! Посилання з такою адресою вже існує!";
                return RedirectToAction("GetAll");
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = "Помилка редагування! Недопустимий символ в посиланні!";
                return RedirectToAction("GetAll");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> GetStatistics([FromForm] GetStatisticsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            switch (query.StatisticType)
            {
                case StatisticType.ByDate:
                    ViewBag.Statistics = result.DateStatistics;
                    return View("Stats/ByDate");
                case StatisticType.ByCountry:
                    ViewBag.Statistics = result.CountryStatistics;
                    return View("Stats/ByCountry");
                case StatisticType.ByOS:
                    ViewBag.Statistics = result.OsStatistics;
                    return View("Stats/ByOs");
                case StatisticType.ByBrowser:
                    ViewBag.Statistics = result.BrowserStatistics;
                    return View("Stats/ByBrowser");
            }
            return RedirectToAction("GetAll");
        }
    }
}
