using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application.Users.Commands.LoginUser;
using Shortener.Application.Users.Commands.RegisterUser;
using Shortener.WebApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Shortener.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        public UserController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserCommand command, CancellationToken token)
        {
            var result = await Mediator.Send(command, token);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Login(LoginUserDto loginDto)
        {
            var command = _mapper.Map<LoginUserCommand>(loginDto);
            var identity = Mediator.Send(command);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity.Result));
            return Ok("Logged In");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Test()
        {
            return Ok(User.Identity.IsAuthenticated);
        }
    }
}
