using AGADEapp.Data.Configration;
using AGADEapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using AGADEapp.Services;
using System.ComponentModel;
using AGADEapp.Models.InputModels;

namespace AGADEapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        private static IWebHostEnvironment _webHostEnvironment;
        public FileController(FileDBContext dBContext, IFileService fileService, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _fileService = fileService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        //Zwraca wszystkie dozwolone do wglądu wpisy DataFile na podstawie uprawnień po podanym userId
        [HttpGet]
        [DisplayName("Get All")]
        public async Task<IEnumerable<DataFile>> GetAll([FromQuery] int? userId)
        {
            if (userId == null)
            {
                return await _fileService.GetFiles(null, null);
            }
            else
            {
                return await _fileService.GetFiles(await _userService.IsAdmin(userId), await _userService.GetUserName(userId));
            }
        }

        //Tworzy wpis DataFile z podaną nazwą i autorem, zwraca status 401 w przypadku wybrania statusu innego niż publiczny przez użytkownika anonimowego
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromForm]DataFileInit file, [FromQuery] int userId)
        {
            if (_userService.GetUserName(userId) == null)
            {
                if (file.Status != 0)
                {
                    return Unauthorized();
                }
            }

            var created = await _fileService.CreateFile(DataFile.of(file, await _userService.GetUserName(userId)));
            return created == null ? NotFound() : Ok(created);
        }

        //zapisuje w katalogu wwwroot przesłany plik, oraz jego informacjie w odpowiednim wpisie DataFile, przyznaje dostęp na podstawie ID użytkownika
        [HttpPost]
        [Route("{fileId}/Upload_attachment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Upload([FromRoute]int fileId, [FromForm]UploadFile obj, [FromForm] int userId)
        {
            if (obj.file.Length > 0)
            {
                if (!Authorize(userId, fileId, _fileService.GetFileById(fileId).Result.Status))
                {
                    return Unauthorized();
                }
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Files\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Files\\");
                    }

                    using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Files\\" + obj.file.FileName))
                    {
                        obj.file.CopyTo(fileStream);
                        fileStream.Flush();

                        await _fileService.Upload(await _userService.GetUserName(userId), obj, fileId);

                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else { return BadRequest(); }
        }

        //Sprawdza czy podany użytkownik jest upoważniony do pobrania załączonego pliku oraz przesyła go po poprawnej autoryzacji
        [HttpGet("{fileId}/Download_attachment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(int fileId, [FromQuery] int? userId)
        {
            var file = await _fileService.GetFileById(fileId);

            if (file == null)
            {
                return NotFound();
            }

            if (!Authorize(userId, fileId, file.Status))
            {
                return Unauthorized();
            }

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", file.Content);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                await fileStream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            await _fileService.ConfirmDownload(await _userService.GetUserName(userId), fileId);

            return File(memoryStream, file.ContentType, file.Content);
        }

        //Sprawdza czy załączony użytkownik jest administratorem oraz usuwa plik po poprawnej weryfikacji
        [HttpDelete("{fileId}/Remove_attachment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAttachment(int fileId, [FromQuery] int? userId)
        {
            var file = await _fileService.GetFileById(fileId);

            if (file == null)
            {
                return NotFound();
            }

            if (await _userService.IsAdmin(userId) == false)
            {
                return Unauthorized();
            }

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", file.Content);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            System.IO.File.Delete(filePath);

            await _fileService.RemoveAttachment(await _userService.GetUserName(userId), fileId);

            return Ok();
        }

        //Sprawdza czy podany użytkownik jest upoważniony do pobrania załączonego pliku oraz przesyła go po poprawnej autoryzacji
        [HttpGet("{fileId}/See_history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ShowHistory(int fileId, [FromQuery] int userId)
        {
            if (!await _userService.IsAdmin(userId))
            {
                return Unauthorized();
            }
            var history = await _fileService.GetFileHistory(fileId);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        //Aktualizuje informacje o danym pliku w oparciu o uprawnienia podanego użytkownika
        [HttpPut("{fileId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFile(int fileId, DataFileInit file, [FromQuery] int userId)
        {
            if(!await _userService.IsAdmin(userId) && !await _fileService.IsOwner(await _userService.GetUserName(userId), fileId))
            {
                return Unauthorized();
            }
            var _file = await _fileService.UpdateFile(fileId, DataFile.of(file, await _userService.GetUserName(userId)), await _userService.GetUserName(userId));
            return _file == null ? NotFound() : Ok(file);
        }

        //Usuwa dany wpis o pliku (tylko admin)
        [HttpDelete("{fileId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int fileId, [FromQuery] int userId)
        {
            if (await _userService.IsAdmin(userId))
            {
                return Unauthorized();
            }
            try
            {
                await _fileService.DeleteFile(fileId);
            } catch 
            {
                return BadRequest();
            }
            return Ok();
        }

        //Zwraca czy podany użytkownik jest upoważniony do danego pliku
        private bool Authorize(int? userid, int fileid, FileStatus status)
        {
            var isowner = _fileService.IsOwner(_userService.GetUserName(userid).Result, fileid);
            var isadmin = _userService.IsAdmin(userid).Result;

            if (isowner.Result || isadmin)
            {
                return true;
            }
            if (userid != null && status != FileStatus.Confidential)
            {
                return true;
            }
            return false;
        }
    }
}
