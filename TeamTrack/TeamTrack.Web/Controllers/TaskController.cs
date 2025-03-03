using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamTrack.Web.Models;
using TeamTrack.Dominio;

namespace TeamTrack.MVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly HttpClient _httpClient;

        public TaskController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreaTask(RichiestaTask request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/task/CreaTask", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Errore nella creazione della task.");
            return View(request);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminaTask(int taskId)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:5001/api/task/EliminaTask/{taskId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Progetto");
            }

            ModelState.AddModelError(string.Empty, "Errore nell'eliminazione della task.");
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AggiungiUtenteATask(int taskId, int userId)
        {

            var response = await _httpClient.PostAsync($"https://localhost:5001/api/task/{taskId}/utente/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Progetto");
            }
            ModelState.AddModelError(string.Empty, "Errore nell'aggiunta dell'utente alla task.");
            return View();

        }

        [HttpDelete]
        public async Task<IActionResult> RimuoviUtenteDaTask(int taskId, int userId)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:5001/api/task/{taskId}/utente/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Progetto");
            }

            ModelState.AddModelError("", "Errore nella rimozione dell'utente dalla task.");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByProgetto(string progettoId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://localhost:5001/api/task//progetto/{progettoId}");
                var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskProgetto>>(response);
                return View(tasks);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }



    }
}
