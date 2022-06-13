using Core.DTO;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCredentials credentials)
        {
            ApplicationUser user = await _authService.CreateAsync(credentials);

            return new JsonResult(new
            {
                id = user.Id,
                email = user.Email,
                roles = user.Roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserCredentials credentials)
        {
            ApplicationUser user = await _authService.PasswordSignInAsync(credentials);

            return new JsonResult(new
            {
                id = user.Id,
                email = user.Email,
                roles = user.Roles
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return Ok();
        }

    }
}
