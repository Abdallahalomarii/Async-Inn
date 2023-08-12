using AsyncInn.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace AsyncInn.Models.InterFaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterDTO Data, ModelStateDictionary modelState, ClaimsPrincipal User);

        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);

    }
}
