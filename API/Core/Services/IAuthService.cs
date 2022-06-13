using Core.DTO;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuthService
    {

        Task<ApplicationUser> CreateAsync(UserCredentials credentials);

        Task<ApplicationUser> PasswordSignInAsync(UserCredentials credentials);

        Task SignOutAsync();
    }
}
