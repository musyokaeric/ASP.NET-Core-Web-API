using Microsoft.AspNetCore.Mvc;
using NZWalks.Web.Models.DTO;

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
    }
}
