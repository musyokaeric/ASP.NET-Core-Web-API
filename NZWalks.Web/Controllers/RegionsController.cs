using Microsoft.AspNetCore.Mvc;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NZWalks.Web.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            List<RegionDto> regions = new List<RegionDto>();
            try
            {
                // Get all Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponse = await client.GetAsync("https://localhost:7001/api/regions");
                httpResponse.EnsureSuccessStatusCode();

                regions.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(regions);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7001/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponse = await client.SendAsync(httpRequestMessage);
            httpResponse.EnsureSuccessStatusCode();

            var region = await httpResponse.Content.ReadFromJsonAsync<RegionDto>();

            if (region != null) return RedirectToAction("Index", "Regions");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7001/api/regions/{id.ToString()}");

            if (response != null) return View(response);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto region)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7001/api/regions/{region.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(region), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response != null) return RedirectToAction("Edit", "Regions");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto region)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7001/api/regions/{region.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                // Console
            }

            return View("Edit");
        }
    }
}
