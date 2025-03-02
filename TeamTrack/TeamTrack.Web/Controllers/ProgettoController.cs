using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamTrack.Web.Models;

namespace TeamTrack.MVC.Controllers
{
    public class ProgettoController : Controllers
    {
        private readonly HttpClient _httpClient;
        public ProgettoController (HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        [HttpGet] 
        public IActionResult CreaProgetto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreaProgetto (RichiestaProgetto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/api/progetti/CreaProgetto", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Creazione del progetto fallita.");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetProgetto(int id, int userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:5001/api/progetti/{id}?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var progetto = await response.Content.ReadAsAsync<ProgettoModel>();
                return View(progetto);
            }

            ModelState.AddModelError(string.Empty, "Progetto non trovato.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AggiornaDataFineProgetto(int id, DateTime nuovaDataFine, int userId)
        {
            var payload = new { nuovaDataFine, userId };
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:5001/api/progetti/{id}", payload);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Progetto", new { id, userId });
            }

            ModelState.AddModelError(string.Empty, "Impossibile aggiornare la data di fine del progetto.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EliminaProgetto(int id, int adminId)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:5001/api/progetti/{id}?adminId={adminId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Impossibile eliminare il progetto.");
            return View();
        }

    }
}
