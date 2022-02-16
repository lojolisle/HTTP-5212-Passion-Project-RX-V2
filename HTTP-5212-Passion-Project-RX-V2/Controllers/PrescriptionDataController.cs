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

        // GET: api/PrescriptionData
        [HttpGet]
        public IQueryable<PrescriptionDto> GetPrescriptions()
        {
            var prescriptions = from p in db.Prescriptions
                select new PrescriptionDto()
                {
                    DoctorName = p.DoctorName,
                    PatientName = p.FirstName + " " + p.LastName
                };
            return prescriptions;

        }

        [HttpGet]
        [ResponseType(typeof(PrescriptionDto))]
        public IHttpActionResult ListPrescriptions()
        {
            List<Prescription> prescriptions = db.Prescriptions.ToList();
            List<PrescriptionDto> prescriptionDtos = new List<PrescriptionDto>();


            prescriptions.ForEach(p => prescriptionDtos.Add(new PrescriptionDto()
            {
                DoctorName = p.DoctorName,
                PatientName = p.FirstName + ' ' + p.LastName,
               
            }));
            return Ok(prescriptionDtos);

        }

        [HttpGet]
        [ResponseType(typeof(PrescriptionDto))]
        public IHttpActionResult GetPrescription(int id)
        {
            List<Prescription> prescriptions = db.Prescriptions.Where(p => p.Drugs.Any(d => d.PrescriptionID == id)).ToList();
            List<PrescriptionDto> prescriptionDtos = new List<PrescriptionDto>();

            prescriptions.ForEach(p => prescriptionDtos.Add(new PrescriptionDto()
            {
                DoctorName = p.DoctorName,
                PatientName = p.FirstName + " " + p.LastName,
            }));
            return Ok(prescriptionDtos);

        }

       /* [HttpGet]
        [Route("api/Prescriptiondata/GetPrescriptionDrug/PId")]
        [ResponseType(typeof(PrescriptionDto))]
        public IHttpActionResult GetPrescriptionDrug(int PId)
        {
            Prescription SelectedPrescription = db.Prescriptions.Include(p => p.Drugs).Where(p => p.ID == PId).FirstOrDefault();
           //PrescriptionDrug SelectedPrescriptionDrug = db.PrescriptionsDrugs.Find(PDId);

            if (SelectedPrescription == null)
            {
                return NotFound();
            }

           // Debug.WriteLine("Selected Drug Qty for prescription ID " + PId + " is " + SelectedPrescriptionDrug.Quantity);
           // Debug.WriteLine("Selected Dpctor name for given pres is " + SelectedPrescriptionDrug.Prescription.DoctorName);

            return Ok();

        }

        */
        //GET: api/PrescriptionData/ListPrescriptions
        /*[HttpGet]
        [ResponseType(typeof(PrescriptionDto))]
        [Route("api/PrescriptionData/ListPrescriptions/1")]
        public IHttpActionResult ListPrescriptions(int pid)
        {
            List<Prescription> prescriptions = db.Prescriptions.Where(p => p.Drugs.Any(d => d.PrescriptionID == pid)).ToList();
            List<PrescriptionDto> prescriptionDtos = new List<PrescriptionDto>();

            prescriptions.ForEach(p => prescriptionDtos.Add(new PrescriptionDto()
            {
               PatientName = p.FirstName + " " + p.LastName,
               Qty = p.Drugs.Count
            }));
            return Ok(prescriptionDtos);

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
        [ResponseType(typeof(Prescription))]
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