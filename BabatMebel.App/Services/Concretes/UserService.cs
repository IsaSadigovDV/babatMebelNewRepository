using BabatMebel.App.Entities;
using BabatMebel.App.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BabatMebel.App.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> FindUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user.UserName;
        }
    }
}
