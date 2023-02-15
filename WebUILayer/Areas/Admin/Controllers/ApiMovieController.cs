using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebUILayer.Areas.Admin.Models;

namespace WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApiMovieController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                {
                    { "X-RapidAPI-Key", "b3c6ce189amshdb34bf43e88151bp1306ecjsn53b839ad19ee" },
                    { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                },
            };
            var response = await client.SendAsync(request);
            List<ApiMovieViewModel> apiMovie = new();
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            apiMovie = JsonConvert.DeserializeObject<List<ApiMovieViewModel>>(body);
            return View(apiMovie);
        }
    }
}