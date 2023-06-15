using Microsoft.AspNetCore.Mvc;

namespace AGADEapp.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
