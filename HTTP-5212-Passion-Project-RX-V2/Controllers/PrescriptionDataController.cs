using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HTTP_5212_Passion_Project_RX_V2.Models;
using System.Diagnostics;

namespace HTTP_5212_Passion_Project_RX_V2.Controllers
{
    public class PrescriptionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Prescriptions in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Prescriptions in the database
        /// </returns>
        /// <example>
        /// </example>
        // GET: api/PrescriptionData/ListPrescriptions
        [HttpGet]
        [ResponseType(typeof(PrescriptionDto))]
        public IHttpActionResult ListPrescriptions()
        {
            List<Prescription> prescriptions = db.Prescriptions.ToList();
           
            List<PrescriptionDto> prescriptionDtos = new List<PrescriptionDto>();
       
            prescriptions.ForEach(p => prescriptionDtos.Add(new PrescriptionDto()
            {
                ID = p.ID,
                PatientFirstName = p.FirstName,
                PatientLastName = p.LastName,
                DoctorName = p.DoctorName,
                Status = p.Status,
                CreatedDate = p.CreatedDate,
            })); 
            return Ok(prescriptionDtos);
        }


        // GET: api/PrescriptionData/GetAllDoctors
        [ResponseType(typeof(PrescriptionDto))]
        [HttpGet]
        public IHttpActionResult GetAllDoctors()
        {
            List<Prescription> prescriptions = db.Prescriptions.ToList();
            List<PrescriptionDto> allDoctors = new List<PrescriptionDto>();

            prescriptions.ForEach(p => allDoctors.Add(new PrescriptionDto()
            {
                ID = p.ID,
                DoctorName = p.DoctorName
            }));

            return Ok(allDoctors);
        }

        // GET: api/PrescriptionData/GetAllPatients
        [ResponseType(typeof(PrescriptionDto))]
        [HttpGet]
        public IHttpActionResult GetAllPatients()
        {
            List<Prescription> prescriptions = db.Prescriptions.ToList();
            List<PrescriptionDto> prescriptionDetails = new List<PrescriptionDto>();

            prescriptions.ForEach(p => prescriptionDetails.Add(new PrescriptionDto()
            {
                ID = p.ID,
                PatientFullName = p.FirstName + " " + p.LastName
            }));

            return Ok(prescriptionDetails);

        }

        /// <summary>
        /// Returns a Prescription mapping givrn ID in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Prescription in the system matching up to the Prescription ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Prescription</param>
        /// <example>
        /// GET: api/PrescriptionData/FindPrescription/5
        /// </example>
        [ResponseType(typeof(PrescriptionDto))]
        [HttpGet]
        public IHttpActionResult FindPrescription(int id)
        {
            Prescription Prescription = db.Prescriptions.Find(id);
            PrescriptionDto PrescriptionDto = new PrescriptionDto()
            {
                ID = Prescription.ID,
                DoctorName = Prescription.DoctorName,
                PatientFirstName = Prescription.FirstName,
                PatientLastName =  Prescription.LastName,
                Status = Prescription.Status,
                CreatedDate = Prescription.CreatedDate
            };
            if (Prescription == null)
            {
                return NotFound();
            }

            return Ok(PrescriptionDto);
        }


        /// <summary>
        /// Updates a particular Prescription in the system with POST Data Input
        /// </summary>
        /// <param name="id">Represents the PrescriptionID Primary Key</param>
        /// <param name="drug">JSON Form Data of a Prescription including id of Prescription to be updated</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response) or
        /// HEADER: 400 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/PrescriptionData/updatePrescription/5
        /// Drug Json Object
        /// </example>
        [Route("api/PrescriptionData/UpdatePrescription/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePrescription(int id, Prescription prescription)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescription.ID)
            {
                return BadRequest();
            }

            db.Entry(prescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!PrescriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult UpdatePrescriptionStatus(int id, Prescription prescription)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescription.ID)
            {
                return BadRequest();
            }

            prescription.CreatedDate = DateTime.Today;
            prescription.Status = true;

            db.Entry(prescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!PrescriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Add a new Prescription to the system
        /// </summary>
        /// <param name="Prescription">JSON form data of a new Prescription (no id)</param> 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Prescription Id, Prescription Data
        /// or
        /// HEADER: 404 (Bad request)
        /// </returns>
        /// <example>
        /// POST: api/PrescriptionData/addNewPrescription
        /// FORM DATA: Prescription json object
        /// </example>
        [ResponseType(typeof(Prescription))]
        [HttpPost]
        public IHttpActionResult AddNewPrescription(Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            prescription.CreatedDate = DateTime.Today;
            // this is processed status which will be true only when it is processed by Pharmacy
            prescription.Status = false; 
            db.Prescriptions.Add(prescription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prescription.ID }, prescription);
          
        }


        /// <summary>
        /// Deletes a prescription from the system matching to the given prescription Id
        /// </summary>
        /// <param name="id">The primary key of the prescription</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///  DELETE: api/PrescriptionData/deletePrescription/5
        /// </example>
        [Route("api/PrescriptionData/deletePrescription/{id}")]
        [ResponseType(typeof(Prescription))]
        [HttpPost]

        public IHttpActionResult DeletePrescription(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return NotFound();
            }

            db.Prescriptions.Remove(prescription);
            db.SaveChanges();

            return Ok(prescription);
        }

        // GET: api/PrescriptionDrugData
        public IQueryable<Prescription> GetPrescriptions()
        {
            return db.Prescriptions;
        }

        // GET: api/PrescriptionData ( another way of handling)
        /*[HttpGet]
        public IQueryable<PrescriptionDto> GetPrescriptions()
        {
            var prescriptions = from p in db.Prescriptions
                select new PrescriptionDto()
                {
                    DoctorName = p.DoctorName,
                    PatientName = p.FirstName + " " + p.LastName     
                };
            return prescriptions;

        }*/


        [HttpGet]
        //[ResponseType(typeof(PrescriptionDto))]
        [Route("api/Prescriptiondata/pDdetails/PId/PDId")]

        public IHttpActionResult PDdetails(int PId, int PDId)
        {
            Prescription SelectedPrescription = db.Prescriptions.Include(p => p.Drugs).Where(p => p.ID == PId).FirstOrDefault();
            PrescriptionDrug SelectedPrescriptionDrug = db.PrescriptionsDrugs.Find(PDId);

            if (SelectedPrescription == null || SelectedPrescriptionDrug == null)
            {
                return NotFound();
            }

            Debug.WriteLine("Selected Drug Qty for prescription ID " + PId + " is " + SelectedPrescriptionDrug.Quantity);
            Debug.WriteLine("Selected Dpctor name for given pres is " + SelectedPrescriptionDrug.Prescription.DoctorName);

            return Ok();
        }

        // GET: api/PrescriptionData/5
        [ResponseType(typeof(Prescription))]
        public IHttpActionResult GetPrescriptionRegular(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(prescription);
        }

        // PUT: api/PrescriptionData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrescription(int id, Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescription.ID)
            {
                return BadRequest();
            }

            db.Entry(prescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PrescriptionData
        [ResponseType(typeof(Prescription))]
        public IHttpActionResult PostPrescription(Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prescriptions.Add(prescription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = prescription.ID }, prescription);
        }

        // DELETE: api/PrescriptionData/5
        /*[ResponseType(typeof(Prescription))]
        public IHttpActionResult DeletePrescription(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return NotFound();
            }

            db.Prescriptions.Remove(prescription);
            db.SaveChanges();

            return Ok(prescription);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrescriptionExists(int id)
        {
            return db.Prescriptions.Count(e => e.ID == id) > 0;
        }
    }
}