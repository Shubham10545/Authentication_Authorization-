using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
         
        public async Task<IActionResult> Index(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await httpClient.GetAsync("https://localhost:7060/WeatherForecast");
                if (response.IsSuccessStatusCode)
                {
                    string weatherDataJson = await response.Content.ReadAsStringAsync();
                    List<WeatherDataModel> weatherDataList = JsonConvert.DeserializeObject<List<WeatherDataModel>>(weatherDataJson);
                    ViewBag.WeatherData = weatherDataList;
                    return View("Index");
                }
                else
                {
                    return View("Error");
                }
            }
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}