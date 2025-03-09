using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TeamTrackProject.Models;

namespace TeamTrackProject.Controllers
{
    public class AutController : Controller
    {
     
        public IActionResult Login()
        {
            return View();
        }

       
        public IActionResult Registrazione()
        {
            return View(); 
        }
    }
}
