using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Users.Commands.LoginUser;
using Shortener.Application.Users.Commands.RegisterUser;
using Shortener.WebApi.Models;
using System.Security.Claims;

namespace Shortener.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        public UserController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public ViewResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromForm] RegisterUserCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidDataException("Input is incorrect");
                }

                var result = await Mediator.Send(command, cancellationToken);
                return RedirectToAction("Login");
            }
            catch (InvalidDataException)
            {
                return View();
            }
            catch (DbUpdateException e)
            {
                if(e.InnerException.Message.Contains("UNIQUE constraint failed") || e.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                    ViewBag.ErrorMessage = "Користувач з таким ім'ям вже існує!";
                else ViewBag.ErrorMessage = "Некоректні дані!";
                return View();
            }
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromForm] LoginUserDto loginDto,
            CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidDataException("Input is incorrect");
                }

                var command = _mapper.Map<LoginUserCommand>(loginDto);
                var identity = Mediator.Send(command, cancellationToken);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity.Result));
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidDataException)
            {
                return View();
            }
            catch (AggregateException e)
            {
                ViewBag.InputError = "Неправильне ім'я користувача або пароль!";
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
