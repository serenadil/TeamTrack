using Azure;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TeamTrackProject.Models.Servizi
{
    public class ServiziQuickChart
    {
        private const string QuickChartUrl = "https://quickchart.io/chart";
        private readonly HttpClient _httpClient;

        public ServiziQuickChart(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Metodo per ottenere il grafico a torta
        public async Task<string> GetStatoTaskGraficoAsync(int[] values)
        {
            var chartConfig = new
            {
                type = "pie",
                data = new
                {
                    datasets = new[]
                    {
                        new
                        {
                            data = values,
                            backgroundColor = new[]
                            {
                                "rgb(255, 99, 132)",
                                "rgb(255, 159, 64)",
                                "rgb(0, 205, 86)"
                            },
                            label = "Task iniziate"
                        }
                    },
                    labels = new[] { "Da fare", "In corso", "Completate" }
                }
            };

            return await GenerateChartUrlAsync(chartConfig);
        }

        // Metodo per ottenere il grafico a barre
        public async Task<string> GetTasksGraficoAsync(int[] tasksPerMonth)
        {
            var chartConfig = new
            {
                type = "bar",
                data = new
                {
                    labels = new[] { "Gen", "Feb", "Mar", "Apr", "Mag", "Giu", "Lug", "Ago", "Set", "Ott", "Nov", "Dic" },
                    datasets = new[]
                    {
                        new
                        {
                            label = "Task iniziate",
                            data = tasksPerMonth,
                            backgroundColor = new[]
                            {
                                "rgba(255, 99, 132, 0.7)",
                                "rgba(255, 159, 64, 0.7)",
                                "rgba(0, 205, 86, 0.7)",
                                "rgba(255, 99, 132, 0.7)",
                                "rgba(255, 159, 64, 0.7)",
                                "rgba(0, 205, 86, 0.7)",
                                "rgba(255, 99, 132, 0.7)",
                                "rgba(255, 159, 64, 0.7)",
                                "rgba(0, 205, 86, 0.7)",
                                "rgba(255, 99, 132, 0.7)",
                                "rgba(255, 159, 64, 0.7)",
                                "rgba(0, 205, 86, 0.7)"
                            },
                            borderColor = new[]
                            {
                                "rgb(255, 99, 132)",
                                "rgb(255, 159, 64)",
                                "rgb(0, 205, 86)",
                                "rgb(255, 99, 132)",
                                "rgb(255, 159, 64)",
                                "rgb(0, 205, 86)",
                                "rgb(255, 99, 132)",
                                "rgb(255, 159, 64)",
                                "rgb(0, 205, 86)",
                                "rgb(255, 99, 132)",
                                "rgb(255, 159, 64)",
                                "rgb(0, 205, 86)"
                            },
                            borderWidth = 1
                        }
                    }
                },
                options = new
                {
                    scales = new
                    {
                        y = new { beginAtZero = true }
                    },
                    title = new
                    {
                        display = true,
                        text = "Task iniziate negli ultimi 12 mesi"
                    }
                }
            };

            return await GenerateChartUrlAsync(chartConfig);
        }

        // Metodo per generare l'URL del grafico
        private async Task<string> GenerateChartUrlAsync(object chartConfig)
        {
            var requestBody = new
            {
                chart = chartConfig
            };

            // Serializza l'oggetto in JSON
            string jsonRequestBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            try
            {
                // Invio la richiesta POST con il corpo JSON
                HttpResponseMessage response = await _httpClient.PostAsync(QuickChartUrl, content);

                // Assicurati che la risposta sia ok
                response.EnsureSuccessStatusCode();

                // Controlla il tipo di contenuto della risposta
                var contentType = response.Content.Headers.ContentType.MediaType;

                if (contentType == "image/png")
                {
                    // Restituisci l'URL del grafico (come immagine)
                    return response.RequestMessage.RequestUri.ToString();
                }
                else
                {
                    throw new Exception($"Errore: La risposta non è un'immagine (tipo: {contentType}).");
                }
            }
            catch (HttpRequestException e)
            {
                // Gestione degli errori HTTP
              
                throw new Exception($"Errore nella chiamata API: {e.Message}.");
            }
        }
    }
}
