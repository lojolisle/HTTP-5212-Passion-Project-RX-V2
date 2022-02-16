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
    public class PrescriptionDrugController : Controller
    {
        private static readonly HttpClient client;

        static PrescriptionDrugController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/PrescriptionDrugData/");
        }

        // GET: PrescriptionDrug/List
        public ActionResult List()
        {
            //objective: communuicate with PrescriptionDrug data api to retrieve a list of Prescriptions
            //curl: https://localhost:44379/api/PrescriptionDrugData/ListPrescriptionDrugs
            //HttpClient client = new HttpClient(){ };
            string url = "ListPrescriptionDrugs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("status is " + response.StatusCode);

            IEnumerable<PrescriptionDrugDto> prescriptionDrugs = response.Content.ReadAsAsync<IEnumerable<PrescriptionDrugDto>>().Result;
            Debug.WriteLine(" No of Prescriptions " + prescriptionDrugs.Count());
            return View(prescriptionDrugs);
        }

        // GET: PrescriptionDrug/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrescriptionDrug/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrescriptionDrug/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrescriptionDrug/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrescriptionDrug/Edit/5
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

        // GET: PrescriptionDrug/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrescriptionDrug/Delete/5
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
