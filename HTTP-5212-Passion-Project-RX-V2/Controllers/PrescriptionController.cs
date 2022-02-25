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
    public class PrescriptionController : Controller
    {
        private static readonly HttpClient client;

        static PrescriptionController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/");
        }


        // GET: Prescription/List
        public ActionResult List()
        {
            //objective: communuicate with Prescription data api to retrieve a list of prescriptions
            //curl: https://localhost:44379/api/PrescriptionData/ListPrescriptions
            //HttpClient client = new HttpClient(){ };
            string url = "PrescriptionData/ListPrescriptions";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("status is " + response.StatusCode);

            IEnumerable<PrescriptionDto> prescriptions = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            return View(prescriptions);
        }


        // GET: Prescription/PharmacyPrescriptionList
        public ActionResult PharmacyPrescriptionList()
        {
            //objective: communuicate with Prescription data api to retrieve a list of prescriptions
            //curl: https://localhost:44379/api/PrescriptionData/ListPrescriptions
            //HttpClient client = new HttpClient(){ };
            string url = "PrescriptionData/ListPrescriptions";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("status is " + response.StatusCode);

            IEnumerable<PrescriptionDto> prescriptions = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            return View(prescriptions);
        }



        // GET: Drug/Details/5
        public ActionResult Details(int id)
        {
            // objective : communicate with Deug api to retrieve one drug for given id
            //curl:  https://localhost:44379/api/PrescriptionData/

            string url = "PrescriptionData/FindPrescription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PrescriptionDto SelectedPrescription = response.Content.ReadAsAsync<PrescriptionDto>().Result;

            return View(SelectedPrescription);
        }


        public ActionResult Error()
        {
            return View();
        }

        // GET: Prescription/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Prescription/Create
        [HttpPost]
        public ActionResult Create(Prescription prescription)
        {
            //curl -k  -d @newdrug.json -H "Content-type:application/json" https://localhost:44379/api/PrescriptionData/AddNewPrescription
            string url = "PrescriptionData/AddNewPrescription";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(prescription);
            Debug.WriteLine("--- in create -- payload ");
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine("--- status code ---");
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        //GET: Drug/Edit/5

        public ActionResult Edit(int id)
        {
            Debug.WriteLine(" in EDIT pres ID " + id);
            string url = "PrescriptionData/FindPrescription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PrescriptionDto SelectedPrescription = response.Content.ReadAsAsync<PrescriptionDto>().Result;

            return View(SelectedPrescription);
        }

        // Post: Prescription/Update/5
        public ActionResult Update(int id, Prescription prescription)
        {
            string url = "PrescriptionData/UpdatePrescription/" + id;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(prescription);

            Debug.WriteLine("---- json payload ----");
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


        //updateProcessedStatus
        public ActionResult UpdatePrescriptionStatus(int id, Prescription prescription)
        {
            string url = "PrescriptionData/UpdatePrescriptionStatus/" + id;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(prescription);

            Debug.WriteLine("---- json payload UpdatePrescriptionStatus----");
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            Debug.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("PharmacyPrescriptionList");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Drug/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "PrescriptionData/FindPrescription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PrescriptionDto selectedPrescription = response.Content.ReadAsAsync<PrescriptionDto>().Result;

            return View(selectedPrescription);
        }


        // POST: Prescription/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "PrescriptionData/DeletePrescription/" + id;
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




















        // GET: Prescription
      /*  public ActionResult Index()
        {
            return View();
        }*/

        // GET: Prescription/Details/5
       /* public ActionResult Details(int id)
        {
            return View();
        }*/

        // GET: Prescription/Create
      /*  public ActionResult Create()
        {
            return View();
        }
      */
        // POST: Prescription/Create
      /*  [HttpPost]
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
        }*/

        // GET: Prescription/Edit/5
      /*  public ActionResult Edit(int id)
        {
            return View();
        }*/

        // POST: Prescription/Edit/5
      /*  [HttpPost]
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
        }*/

        // GET: Prescription/Delete/5
      /*  public ActionResult Delete(int id)
        {
            return View();
        }
      */
        // POST: Prescription/Delete/5
     /*   [HttpPost]
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
        }*/
    }
}
