using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AsyncInn.Models.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Authenticate(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            bool checkPassword = await _userManager.CheckPasswordAsync(user, password);

            if (checkPassword)
            {
                return new UserDTO
                {
                    ID = user.Id,
                    Username = userName
                };
            }
            return null;
        }

        public async Task<UserDTO> Register(RegisterDTO Data , ModelStateDictionary modelState)
        {
            var user = new User()
            {
                UserName = Data.UserName,
                Email = Data.Email,
                PhoneNumber = Data.PhoneNumber

            };

            var result = await _userManager.CreateAsync(user,Data.Password);

            if (result.Succeeded)
            {
                return new UserDTO
                {
                    ID = user.Id,
                    Username = user.UserName

                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(Data.Password) :
                                error.Code.Contains("Email") ? nameof(Data.Email) :
                                error.Code.Contains("UserName") ? nameof(Data.UserName) :
                                "";

                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }
    }
}
