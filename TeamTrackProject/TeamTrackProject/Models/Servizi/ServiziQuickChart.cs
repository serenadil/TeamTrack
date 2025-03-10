using System.Text.Json;
using System.Text;

namespace TeamTrackProject.Models.Servizi
{
    public class ServiziQuickChart
    {
        private const string QuickChartUrl = "https://quickchart." +
            "io/chart";
        private readonly HttpClient _httpClient;

        public ServiziQuickChart(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStatoTaskGrafico(int[] values)
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

        public async Task<string> GetTasksGrafico(int[] tasksPerMonth)
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

        private async Task<string> GenerateChartUrlAsync(object chartConfig)
        {
            string jsonConfig = JsonSerializer.Serialize(chartConfig);
            var content = new StringContent($"{{\"chart\": {jsonConfig} }}", Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(QuickChartUrl, content);
            Console.Write(response);
            Console.Write(response.Content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Errore nella chiamata QuickChart: {response.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}