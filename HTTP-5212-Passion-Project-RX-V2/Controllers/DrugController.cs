using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HTTP_5212_Passion_Project_RX_V2.Models;
using System.Web.Script.Serialization;


namespace HTTP_5212_Passion_Project_RX_V2.Controllers
{
    public class DrugController : Controller
    {
        private static readonly HttpClient client;

        static DrugController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/");
        }

        // GET: Drug/List
        public ActionResult List()
        {
            //objective: communuicate with Drug data api to retrieve a list of drugs
            //curl: https://localhost:44379/api/DrugData/ListDrugs
            //HttpClient client = new HttpClient(){ };
            string url = "DrugData/ListDrugs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("status is " + response.StatusCode);

            IEnumerable<DrugDto> drugs = response.Content.ReadAsAsync<IEnumerable<DrugDto>>().Result;
            // get from DTO instead
            //IEnumerable<DrugDto> drugs = response.Content.ReadAsAsync<IEnumerable<DrugDto>>().Result;
            //Debug.WriteLine(" No of Drugs " + drugs.Count());
            return View(drugs);
        }

        // GET: Drug/Details/5
        public ActionResult Details(int id)
        {
            // objective : communicate with Deug api to retrieve one drug for given id
            //curl:  https://localhost:44379/api/DrugData/

            string url = "DrugData/FindDrug/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DrugDto SelectedDrug = response.Content.ReadAsAsync<DrugDto>().Result;
           

            return View(SelectedDrug);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Drug/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Drug/Create
        [HttpPost]
        public ActionResult Create(Drug drug)
        {
            //curl -k  -d @newdrug.json -H "Content-type:application/json" https://localhost:44379/api/DrugData/addnewdrug
            string url = "DrugData/AddNewDrug";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(drug);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            } else
            {
                return RedirectToAction("Error");
            }
           
        }

        //GET: Drug/Edit/5

        public ActionResult Edit(int id)
        {    
            string url = "DrugData/FindDrug/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DrugDto SelectedDrug = response.Content.ReadAsAsync<DrugDto>().Result;

            return View(SelectedDrug);
        }

        // Post: Drug/Update/5
        public ActionResult Update(int id, Drug drug)
        {
            string url = "DrugData/UpdateDrug/" + id;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(drug);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
  
            Debug.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
           
        }
   

        // GET: Drug/Delete/5

        public ActionResult DeleteConfirm(int id)
        {
            string url = "DrugData/FindDrug/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DrugDto selectedDrug = response.Content.ReadAsAsync<DrugDto>().Result;

            return View(selectedDrug);
        }

        // POST: Drug/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DrugData/DeleteDrug/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            } else
            {
                return RedirectToAction("Error");
            }

        }
    }
}
