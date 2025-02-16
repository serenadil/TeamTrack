using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamTrack.Web.Models;

namespace TeamTrack.MVC.Controllers
{
    public class UtenteController : Controller
    {
        private readonly HttpClient _httpClient;

        public UtenteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Registrazione()
        {
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> Registrazione(RichiestaRegistazione request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/utente/Registrazione", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Registrazione fallita.");
            return View(request);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RichiestaLogin request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/utente/Login", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Credenziali non valide.");
            return View(request);
        }
    }
}
