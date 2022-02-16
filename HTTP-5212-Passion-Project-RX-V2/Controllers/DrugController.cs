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
            client.BaseAddress = new Uri("https://localhost:44379/api/DrugData/");
        }

        // GET: Drug/List
        public ActionResult List()
        {
            //objective: communuicate with Drug data api to retrieve a list of drugs
            //curl: https://localhost:44379/api/DrugData/GetDrugs
            //HttpClient client = new HttpClient(){ };
            string url = "GetDrugs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("status is " + response.StatusCode);

            IEnumerable<Drug> drugs = response.Content.ReadAsAsync<IEnumerable<Drug>>().Result;
            Debug.WriteLine(" No of Drugs " + drugs.Count());
            return View(drugs);
        }

        // GET: Drug/Details/5
        public ActionResult Details(int id)
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
            string url = "AddNewDrug";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(drug);

            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            client.PostAsync(url, content);
            return RedirectToAction("List");
        }

        // GET: Drug/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Drug/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Drug/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Drug/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
