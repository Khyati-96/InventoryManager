using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Inventory_Manager.Models;
using Inventory_Manager.Models.ViewModels;

namespace Inventory_Manager.Controllers
{
    public class ItemController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ItemController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
        }

        // GET: Item/List
        public ActionResult List()
        {
            string url = "itemdata/listitems";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ItemDto> items = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;

            return View(items);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            ItemDto ViewModel = new ItemDto();

            string url = "itemdata/finditem/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ItemDto SelectedItem = response.Content.ReadAsAsync<ItemDto>().Result;
            ViewModel = SelectedItem;

            url = "aisledata/findaisle/" + id;
            response = client.GetAsync(url).Result;
            AisleDto AvailableAisle = response.Content.ReadAsAsync<AisleDto>().Result;

            ViewModel.AvailableAisle = AvailableAisle;

            return View(ViewModel);
        }

        // GET: Item/New
        public ActionResult New()
        {
            ItemViewModel item = new ItemViewModel();

            string url = "aisledata/ListAisle";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<AisleDto> Aisles = response.Content.ReadAsAsync<IEnumerable<AisleDto>>().Result;
            item.AisleList = Aisles;

            return View(item);
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(Item item)
        {
            string url = "itemdata/AddItem";

            string jsonpayload = jss.Serialize(item);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            ItemViewModel ViewModelDto = new ItemViewModel();

            string url = "itemdata/finditem/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ItemDto SelectedItem = response.Content.ReadAsAsync<ItemDto>().Result;
            ViewModelDto.Id = SelectedItem.Id;
            ViewModelDto.ItemName = SelectedItem.ItemName;
            ViewModelDto.ItemType = SelectedItem.ItemType;
            ViewModelDto.BBD = SelectedItem.BBD;
            ViewModelDto.ItemCount = SelectedItem.ItemCount;

            url = "aisledata/ListAisle";
            response = client.GetAsync(url).Result;
            IEnumerable<AisleDto> Aisles = response.Content.ReadAsAsync<IEnumerable<AisleDto>>().Result;
            ViewModelDto.AisleList = Aisles;

            return View(ViewModelDto);
        }

        // POST: Item/Update/5
        [HttpPost]
        public ActionResult Update(int id, Item item)
        {
            string url = "itemdata/updateitem/" + id;
            string jsonpayload = jss.Serialize(item);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            string url = "itemdata/finditem/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ItemDto selecteditem = response.Content.ReadAsAsync<ItemDto>().Result;
            return View(selecteditem);
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "itemdata/deleteitem/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
