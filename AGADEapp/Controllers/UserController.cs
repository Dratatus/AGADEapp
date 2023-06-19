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
        private readonly IFileService _fileService;

        public UserController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        //Zwraca wszystkich użytkowników
        [HttpGet]
        [DisplayName("Get All")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAllUsers();
        }

        //Sprawdza czy dane logowania są poprawne i w pozytywnym przypadku zwraca obiekt User
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] string login, [FromForm] string password)
        {
            var loggedIn = await _userService.Login(login, password);
            return Ok(loggedIn);

        }

        //Wylogowuje użytkownika
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        //Dodaje użytkownika do bazy danych
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromForm] UserRegister user)
        {
            var created = await _userService.Register(user);
            return created == null ? NotFound() : Ok(created);
        }

        //Usuwa użytkownika z bazy danych
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

        //Zwraca wszystkie wpisy DataFile danego użytkownika
        [HttpGet]
        [Route("{userId}/UserFiles")]
        public async Task<IEnumerable<DataFile>> GetAllUser([FromRoute] int? userId)
        {
            return await _fileService.GetMyFiles(await _userService.GetUserName(userId));
        }
    }
}
