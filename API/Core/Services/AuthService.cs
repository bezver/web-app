using Core.DTO;
using Core.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService: IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(IServiceProvider services)
        {
            _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            _signInManager = services.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        public async Task<ApplicationUser> CreateAsync(UserCredentials credentials)
        {
            var user = new ApplicationUser()
            {
                UserName = credentials.Email,
                Email = credentials.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
            {
                //throw new ServiceException(HttpStatusCode.BadRequest, "Email is already in use");
                throw new ServiceException("Email is already in use");
            }

            return await PasswordSignInAsync(credentials);
        }

        public async Task<ApplicationUser> PasswordSignInAsync(UserCredentials credentials)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);
            if (result.Succeeded)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(credentials.Email);
                user.Roles = await _userManager.GetRolesAsync(user);
                return user;
            }
            throw new ServiceException(HttpStatusCode.BadRequest, "Login or Password is incorrect");
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
