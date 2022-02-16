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

namespace HTTP_5212_Passion_Project_RX_V2.Controllers
{
    public class PrescriptionDrugDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PrescriptionDrugData
        public IQueryable<PrescriptionDrug> GetPrescriptionsDrugs()
        {
            return db.PrescriptionsDrugs;
        }


        //GET; api/PrescriptionDrugData/ListPrescriptionDrugs
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        public IHttpActionResult ListPrescriptionDrugs()
        {
            List<PrescriptionDrug> prescriptionDrugs = db.PrescriptionsDrugs.ToList();
            List<PrescriptionDrugDto> prescriptionDrugDtos = new List<PrescriptionDrugDto>();

            prescriptionDrugs.ForEach(pd => prescriptionDrugDtos.Add(new PrescriptionDrugDto()
            {
                PrescriptionID = pd.PrescriptionID,
                DoctorName = pd.Prescription.DoctorName,
                PatientName = pd.Prescription.FirstName + ' ' + pd.Prescription.LastName,
                DrugName = pd.Drug.DrugName,
                Qty = pd.Quantity,
                Repeat = pd.Repeat
            }));
            return Ok(prescriptionDrugDtos);

        }


        //GET; api/PrescriptionDrugData/ListPrescriptionDrugsForPrescription/5
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        public IHttpActionResult ListPrescriptionDrugsForPrescription(int id)
        {
            List<PrescriptionDrug> prescriptionDrugs = db.PrescriptionsDrugs.Where(pd => pd.PrescriptionID == id).ToList();
            List<PrescriptionDrugDto> prescriptionDrugDtos = new List<PrescriptionDrugDto>();

            prescriptionDrugs.ForEach(pd => prescriptionDrugDtos.Add(new PrescriptionDrugDto()
            {
                PrescriptionID = pd.PrescriptionID,
                DoctorName = pd.Prescription.DoctorName,
                PatientName = pd.Prescription.FirstName + ' ' + pd.Prescription.LastName,
                DrugName = pd.Drug.DrugName,
                Qty = pd.Quantity,
                Repeat = pd.Repeat
            }));
            return Ok(prescriptionDrugDtos);

        }


        //GET; api/PrescriptionDrugData/ListPrescriptionDrugsForDrug/5
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        public IHttpActionResult ListPrescriptionDrugsForDrug(int id)
        {
            List<PrescriptionDrug> prescriptionDrugs = db.PrescriptionsDrugs.Where(pd => pd.DrugID == id).ToList();
            List<PrescriptionDrugDto> prescriptionDrugDtos = new List<PrescriptionDrugDto>();

            prescriptionDrugs.ForEach(pd => prescriptionDrugDtos.Add(new PrescriptionDrugDto()
            {
                PrescriptionID = pd.PrescriptionID,
                DoctorName = pd.Prescription.DoctorName,
                PatientName = pd.Prescription.FirstName + ' ' + pd.Prescription.LastName,
                DrugName = pd.Drug.DrugName,
                Qty = pd.Quantity,
                Repeat = pd.Repeat
            }));
            return Ok(prescriptionDrugDtos);

        }

     


        // GET: api/PrescriptionDrugData/5
        [ResponseType(typeof(PrescriptionDrug))]
        public IHttpActionResult GetPrescriptionDrug(int id)
        {
            PrescriptionDrug prescriptionDrug = db.PrescriptionsDrugs.Find(id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            return Ok(prescriptionDrug);
        }

        // PUT: api/PrescriptionDrugData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrescriptionDrug(int id, PrescriptionDrug prescriptionDrug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescriptionDrug.PrescriptionID)
            {
                return BadRequest();
            }

            db.Entry(prescriptionDrug).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionDrugExists(id))
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

        // POST: api/PrescriptionDrugData
        [ResponseType(typeof(PrescriptionDrug))]
        public IHttpActionResult PostPrescriptionDrug(PrescriptionDrug prescriptionDrug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PrescriptionsDrugs.Add(prescriptionDrug);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PrescriptionDrugExists(prescriptionDrug.PrescriptionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = prescriptionDrug.PrescriptionID }, prescriptionDrug);
        }

        // DELETE: api/PrescriptionDrugData/5
        [ResponseType(typeof(PrescriptionDrug))]
        public IHttpActionResult DeletePrescriptionDrug(int id)
        {
            PrescriptionDrug prescriptionDrug = db.PrescriptionsDrugs.Find(id);
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            db.PrescriptionsDrugs.Remove(prescriptionDrug);
            db.SaveChanges();

            return Ok(prescriptionDrug);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrescriptionDrugExists(int id)
        {
            return db.PrescriptionsDrugs.Count(e => e.PrescriptionID == id) > 0;
        }
    }
}