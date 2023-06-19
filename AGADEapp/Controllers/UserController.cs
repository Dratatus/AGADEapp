using AGADEapp.Models;
using AGADEapp.Models.InputModels;
using AGADEapp.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AGADEapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [DisplayName("Get All")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] string login, [FromForm] string password)
        {
            var loggedIn = await _userService.Login(login, password);
            return Ok(loggedIn);

        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromForm] UserRegister user)
        {
            var created = await _userService.Register(user);
            return created == null ? NotFound() : Ok(created);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveUser(int id)
        {
            try
            {
                await _userService.RemoveUser(id);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
