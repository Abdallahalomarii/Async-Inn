using AsyncInn.Models;
using AsyncInn.Models.DTO;
using AsyncInn.Models.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace AsyncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _context;

        public UserController(IUser context)
        {
            _context = context;
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<UserDTO>> SignIn(LoginDTO Data)
        {
            var user = await _context.Authenticate(Data.Username, Data.Password);

            if (user != null)
            {
                return user;
            }
            return Unauthorized();
        }
        //[Authorize(Roles = "District Manager , Property Manager")]
        [HttpPost("register")]

        public async Task<ActionResult<UserDTO>> Register(RegisterDTO Data)
        {
            //if (!User.IsInRole("District Manager") && !User.IsInRole("Property Manager"))
            //{
            //    return Forbid();
            //}
            var newUser = await _context.Register(Data, this.ModelState, User);

            if (ModelState.IsValid)
            {
                if (newUser != null) 
                    return newUser;

                else
                    return NotFound();
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDTO>> Profile()
        {
            var profile = await _context.GetUser(this.User);

            return Ok(profile);
        }


    }
}
