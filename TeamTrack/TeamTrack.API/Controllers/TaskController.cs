using Microsoft.AspNetCore.Mvc;

namespace TeamTrack.API.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
