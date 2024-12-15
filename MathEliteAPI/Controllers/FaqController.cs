using Microsoft.AspNetCore.Mvc;

namespace MathEliteAPI.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
