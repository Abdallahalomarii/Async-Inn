using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Common;
using System.Data;
using System.Security.Claims;

namespace AsyncInn.Models.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<User> _userManager;

        private readonly JwtTokenService _JwtTokenService;

        public UserService(UserManager<User> userManager, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            this._JwtTokenService = jwtTokenService;
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
                    Username = userName,
                    Token = await _JwtTokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }
            return null;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            return new UserDTO
            {
                ID = user.Id,
                Username = user.UserName,
                Token = await _JwtTokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
                Roles = await _userManager.GetRolesAsync(user)

            };
        }

        public async Task<UserDTO> Register(RegisterDTO Data, ModelStateDictionary modelState, ClaimsPrincipal User)
        {
            bool isDistrictManager = User.IsInRole("District Manager");
            bool isPropertyManager = User.IsInRole("Property Manager");

            // Role-specific registration logic
            if (isDistrictManager || (isPropertyManager && Data.Roles.Contains("Agent")))
            {
                var user = new User()
                {
                    UserName = Data.UserName,
                    Email = Data.Email,
                    PhoneNumber = Data.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, Data.Password);

                if (result.Succeeded)
                {
                    // Assign roles based on role-specific conditions
                    if (isDistrictManager)
                    {
                        await _userManager.AddToRolesAsync(user, Data.Roles);
                    }
                    else if (isPropertyManager && Data.Roles.Contains("Agent"))
                    {
                        await _userManager.AddToRolesAsync(user, new[] { "Agent" });
                    }

                    return new UserDTO
                    {
                        ID = user.Id,
                        Username = user.UserName,
                        Token = await _JwtTokenService.GetToken(user, System.TimeSpan.FromSeconds(60)),
                        Roles = await _userManager.GetRolesAsync(user)
                    };
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        var errorKey = error.Code.Contains("Password") ? nameof(Data.Password) :
                                        error.Code.Contains("Email") ? nameof(Data.Email) :
                                        error.Code.Contains("UserName") ? nameof(Data.UserName) :
                                        "";

                        modelState.AddModelError(errorKey, error.Description);
                    }

                    return null; // Return some appropriate response for a failed user creation
                }
            }
            else
            {
                modelState.AddModelError("", "You don't have permission to create this type of account.");
                return null;
            }
        }
    }
}
