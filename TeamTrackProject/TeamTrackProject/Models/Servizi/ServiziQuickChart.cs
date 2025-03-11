using System;
using System.Net;
using System.Net.Http;
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

      
        public async Task<byte[]> GetStatoTaskGraficoAsync(int[] values)
        {
            var chartConfig = new
            {
                type = "pie",
                data = new
                {
                    labels = new[] { "Da fare", "In corso", "Completate" },
                    datasets = new[]
                    {
                        new
                        {
                            data = values,
                            backgroundColor = new[] { "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(0, 205, 86)" }
                        }
                    }
                }
            };

            string jsonString = JsonSerializer.Serialize(chartConfig);
            string encodedJson = WebUtility.UrlEncode(jsonString);

            string url = $"{QuickChartUrl}?c={encodedJson}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Errore nel recupero del grafico. Status Code: {response.StatusCode}");
            }

            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
            return imageBytes;
        }

        public async Task<byte[]> GetTasksGraficoAsync(int[] tasksPerMonth)
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
                                "rgba(54, 162, 235, 0.7)",
                                "rgba(153, 102, 255, 0.7)",
                                "rgba(255, 206, 86, 0.7)",
                                "rgba(75, 192, 192, 0.7)",
                                "rgba(255, 140, 0, 0.7)",
                                "rgba(201, 203, 207, 0.7)",
                                "rgba(255, 99, 132, 0.7)",
                                "rgba(255, 159, 64, 0.7)",
                                "rgba(0, 205, 86, 0.7)"
                            }
                        }
                    }
                }
            };

            string jsonString = JsonSerializer.Serialize(chartConfig);
            string encodedJson = WebUtility.UrlEncode(jsonString);

            string url = $"{QuickChartUrl}?c={encodedJson}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Errore nel recupero del grafico. Status Code: {response.StatusCode}");
            }

            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
            return imageBytes;
        }






    }
}
