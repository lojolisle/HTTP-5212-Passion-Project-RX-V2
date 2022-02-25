using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HTTP_5212_Passion_Project_RX_V2.Models;
using HTTP_5212_Passion_Project_RX_V2.Models.ViewModels;
using System.Web.Script.Serialization;


namespace HTTP_5212_Passion_Project_RX_V2.Controllers
{
    public class PrescriptionDrugController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PrescriptionDrugController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/api/");
        }

        // GET: PrescriptionDrug/List
        public ActionResult List()
        {
            string url = "PrescriptionDrugData/ListPrescriptionDrugs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("status is ----" + response.StatusCode);

            IEnumerable<PrescriptionDrugDto> prescriptionDrugs = response.Content.ReadAsAsync<IEnumerable<PrescriptionDrugDto>>().Result;
            Debug.WriteLine(" No of Prescriptions-------- " + prescriptionDrugs.Count());
            return View(prescriptionDrugs);
        }

        // GET: PrescriptionDrug/AllPrescDetails
        public ActionResult AllPrescDetails()
        {
            AllDetails ViewModel = new AllDetails();

            //get doctor and patient name from precription Data controller
            string url = "PrescriptionData/ListPrescriptions";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PrescriptionDto> SelectedPrescriptions = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            ViewModel.allPrescription = SelectedPrescriptions;

            url = "PrescriptionDrugData/ListPrescriptionDrugs";
            response = client.GetAsync(url).Result;
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);
            IEnumerable<PrescriptionDrugDto> selectedPrescriptionDrugs = response.Content.ReadAsAsync<IEnumerable<PrescriptionDrugDto>>().Result;
            ViewModel.allPrescriptionDrug = selectedPrescriptionDrugs;

            return View(ViewModel);
        }



        // GET: PrescriptionDrug/Details/5
        public ActionResult Details(int id)
        {
            Details ViewModel = new Details();

            //get doctor and patient name from precription Data controller
            string url = "PrescriptionData/FindPrescription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PrescriptionDto SelectedPrescription = response.Content.ReadAsAsync<PrescriptionDto>().Result;
            ViewModel.selectedPrescription = SelectedPrescription;

            url = "PrescriptionDrugData/ListDrugsForPrescription/" + id;
            response = client.GetAsync(url).Result;
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);
            IEnumerable<PrescriptionDrugDto> selectedPrescriptionDrugs = response.Content.ReadAsAsync<IEnumerable<PrescriptionDrugDto>>().Result;
            ViewModel.selectedPrescriptionDrug = selectedPrescriptionDrugs;

            // for adding new drugs to a prescription
            url = "PrescriptionData/GetAllDoctors";
            response = client.GetAsync(url).Result;
            IEnumerable<PrescriptionDto> doctorNames = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            ViewModel.DoctorNames = doctorNames;

            url = "PrescriptionData/GetAllPatients";
            response = client.GetAsync(url).Result;
            IEnumerable<PrescriptionDto> prescriptionDetails = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            ViewModel.PrescriptionDetails = prescriptionDetails;

            url = "DrugData/GetAllDrugs";
            response = client.GetAsync(url).Result;
            IEnumerable<DrugDto> drugDtos = response.Content.ReadAsAsync<IEnumerable<DrugDto>>().Result;
            ViewModel.DrugDetails = drugDtos;

            return View(ViewModel);
        }

        public ActionResult DetailsForPharmacy(int id)
        {
            Details ViewModel = new Details();

            //get doctor and patient name from precription Data controller
            string url = "PrescriptionData/FindPrescription/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PrescriptionDto SelectedPrescription = response.Content.ReadAsAsync<PrescriptionDto>().Result;
            ViewModel.selectedPrescription = SelectedPrescription;

            url = "PrescriptionDrugData/ListDrugsForPrescription/" + id;
            response = client.GetAsync(url).Result;
            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);
            IEnumerable<PrescriptionDrugDto> selectedPrescriptionDrugs = response.Content.ReadAsAsync<IEnumerable<PrescriptionDrugDto>>().Result;
            ViewModel.selectedPrescriptionDrug = selectedPrescriptionDrugs;

            // for adding new drugs to a prescription
            url = "PrescriptionData/GetAllDoctors";
            response = client.GetAsync(url).Result;
            IEnumerable<PrescriptionDto> doctorNames = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            ViewModel.DoctorNames = doctorNames;

            url = "PrescriptionData/GetAllPatients";
            response = client.GetAsync(url).Result;
            IEnumerable<PrescriptionDto> prescriptionDetails = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            ViewModel.PrescriptionDetails = prescriptionDetails;


            url = "DrugData/GetAllDrugs";
            response = client.GetAsync(url).Result;
            IEnumerable<DrugDto> drugDtos = response.Content.ReadAsAsync<IEnumerable<DrugDto>>().Result;
            ViewModel.DrugDetails = drugDtos;

            return View(ViewModel);
        }



        // GET: PrescriptionDrug/New
        public ActionResult New()
        {
            CreateNew ViewModel = new CreateNew();
            string url = "PrescriptionData/GetAllDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PrescriptionDto> doctorNames = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            ViewModel.DoctorNames = doctorNames;

            url  = "PrescriptionData/GetAllPatients";
            response = client.GetAsync(url).Result;
            IEnumerable<PrescriptionDto> prescriptionDetails = response.Content.ReadAsAsync<IEnumerable<PrescriptionDto>>().Result;
            ViewModel.PrescriptionDetails = prescriptionDetails;

      
            url = "DrugData/GetAllDrugs";
            response = client.GetAsync(url).Result;
            IEnumerable<DrugDto> drugDtos = response.Content.ReadAsAsync<IEnumerable<DrugDto>>().Result;
            ViewModel.DrugDetails = drugDtos;

            return View(ViewModel);
        }

        // POST: PrescriptionDrug/Create
        [HttpPost]
        public ActionResult Create(PrescriptionDrug prescriptionDrug)
        {
            Debug.WriteLine("the json payload is :");
          
            //objective: add a new Prescription Drug into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/animaldata/addanimal 
            string url = "PrescriptionDrugData/AddPrescriptionDrug";

            Debug.WriteLine("-----id is ---" + prescriptionDrug.PrescriptionID);
            string jsonpayload = jss.Serialize(prescriptionDrug);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            Debug.WriteLine("--check CONTENT----");
            Debug.WriteLine(content);

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            Debug.WriteLine(response.StatusCode);
            Debug.WriteLine(response.Content.ReadAsAsync<PrescriptionDrug>().Result);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details/" + prescriptionDrug.PrescriptionID);
            }
            else
            {
                return RedirectToAction("Error");
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

        // GET: PrescriptionDrug/DeleteConfirm/pId/dId
        [HttpGet]
        public ActionResult DeleteConfirm(int id, int DrugID)
        {
            /*  DeleteConfirm ViewModel = new DeleteConfirm();
              string url = "PrescriptionDrugData/FindDrugFromPrescription/" + id + "/" + DrugID;
              HttpResponseMessage response = client.GetAsync(url).Result;
              IEnumerable<PrescriptionDrugDto> selectedPDrug = response.Content.ReadAsAsync<IEnumerable<PrescriptionDrugDto>>().Result;
              ViewModel.selectedPDrug = selectedPDrug;*/

            Debug.WriteLine("---in controller deleteconfirm---");
            //Debug.WriteLine(prescriptionId + " " + drugId);
            string url = "PrescriptionDrugData/FindDrugFromPrescription/" + id + "/"+ DrugID;
            Debug.WriteLine(url);
           
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("--- response in controller");
            Debug.WriteLine(response);
            IEnumerable<PrescriptionDrugDto> selectedDrug = response.Content.ReadAsAsync<IEnumerable<PrescriptionDrugDto>>().Result;
           
            return View(selectedDrug);     
        }

        [HttpGet]
        public ActionResult UnAssociateDrug(int id, int DrugID)
        {
            Debug.WriteLine("Attempting to unassociate drug :" + id + " with prescription: " + DrugID);

            //call our api to unassociate drug with presc
            string url = "PrescriptionDrugData/UnAssociateDrug/" + id + "/" + DrugID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }



        // POST: PrescriptionDrug/Delete/5/2
        [HttpPost]
        public ActionResult Delete(int prescriptionId, int DrugID)
        {
            Debug.WriteLine("---- in delete --" + prescriptionId + " " + DrugID);
            string url = "PrescriptionDrugData/DeletePrescriptionDrug/" + prescriptionId + "/" + DrugID;
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
