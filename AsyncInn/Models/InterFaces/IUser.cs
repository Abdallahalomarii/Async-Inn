using AsyncInn.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AsyncInn.Models.InterFaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterDTO Data, ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username, string password);


    }
}
