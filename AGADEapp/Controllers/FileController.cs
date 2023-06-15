using AGADEapp.Data.Configration;
using AGADEapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AGADEapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileDBContext _fileDBcontext;
        public FileController(FileDBContext dBContext)
        {
            _fileDBcontext = dBContext;
        }

        [HttpGet]
        public async Task<IEnumerable<DataFile>> GetAll()
        {
            var files = await _fileDBcontext.DataFile.ToListAsync();

            return files;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(DataFile), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFileById(int id)
        {
            var file = await _fileDBcontext.DataFile.FindAsync(id);

            return file == null ? NotFound() : Ok(file);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Upload(DataFile file)
        {
            await _fileDBcontext.DataFile.AddAsync(file);
            await _fileDBcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFileById), new { id = file.Id }, file);
        }

    }
}
