using Microsoft.AspNetCore.Mvc;
using TeamTrackProject.Models.Servizi;

namespace TeamTrackProject.Models.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuickChartController : ControllerBase
    {
        private readonly ServiziQuickChart _serviziQuickChart;

        public QuickChartController(ServiziQuickChart s)
        {
            _serviziQuickChart = s;
        }



        /// <summary>
        /// Genera un grafico a torta per le task iniziate (da fare, in corso, completate).
        /// I dati devono essere passati come parametri di query: "daFare", "inCorso", "completate".
        /// </summary>
        [HttpGet("pie")]
        public async Task<IActionResult> GetPieChart( int daFare, int inCorso,int completate)
        {
            int[] taskCounts = { daFare, inCorso, completate };

            byte[] imageBytes = await _serviziQuickChart.GetStatoTaskGraficoAsync(taskCounts);

            return File(imageBytes, "image/png");
        }





        /// <summary>
        /// Genera un grafico a barre con le task iniziate negli ultimi 12 mesi.
        /// I dati devono essere passati come parametri di query per ogni mese.
        /// </summary>
        [HttpGet("tasks-participation")]
        public async Task<IActionResult> GetTasksParticipationChart( [FromQuery] int[] tasksPerMonth)
        {
            if (tasksPerMonth.Length != 12)
            {
                return BadRequest("Devi fornire esattamente 12 valori, uno per ogni mese.");
            }

            byte[] imageBytes = await _serviziQuickChart.GetTasksGraficoAsync(tasksPerMonth);
            return File(imageBytes, "image/png");
        }

    }
}