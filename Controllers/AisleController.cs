using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Inventory_Manager.Models;

namespace Inventory_Manager.Controllers
{
    public class AisleController : Controller
    {
        private readonly HttpClient _client;

        public AisleController()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:44382/api/") };
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            HttpResponseMessage response = await _client.GetAsync("aisledata/listaisle");
            response.EnsureSuccessStatusCode();
            var aisles = await response.Content.ReadAsAsync<IEnumerable<AisleDto>>();
            return View(aisles);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"aisledata/findaisle/{id}");
            response.EnsureSuccessStatusCode();
            var selectedAisle = await response.Content.ReadAsAsync<AisleDto>();
            return View(selectedAisle);
        }

        [HttpGet]
        public async Task<ActionResult> New()
        {
            return View(new AisleViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aisle aisle)
        {
            var jsonPayload = new JavaScriptSerializer().Serialize(aisle);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("aisledata/addAisle", content);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"aisledata/findaisle/{id}");
            response.EnsureSuccessStatusCode();
            var selectedAisle = await response.Content.ReadAsAsync<AisleDto>();

            response = await _client.GetAsync("aisledata/ListAisle");
            response.EnsureSuccessStatusCode();
            var aisles = await response.Content.ReadAsAsync<IEnumerable<AisleDto>>();

            var viewModel = new AisleViewModel
            {
                AisleId = selectedAisle.AisleId,
                Name = selectedAisle.Name,
                Desc = selectedAisle.Desc,
                AisleCap = selectedAisle.AisleCap,
                AisleList = aisles
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, Aisle aisle)
        {
            var jsonPayload = new JavaScriptSerializer().Serialize(aisle);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync($"aisledata/updateaisle/{id}", content);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"aisledata/findaisle/{id}");
            response.EnsureSuccessStatusCode();
            var selectedAisle = await response.Content.ReadAsAsync<AisleDto>();

            return View(selectedAisle);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            HttpResponseMessage response = await _client.PostAsync($"aisledata/deleteaisle/{id}", new StringContent(""));
            response.EnsureSuccessStatusCode();

            return RedirectToAction("List");
        }
    }
}
