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
using System.Diagnostics;
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
                Quantity = pd.Quantity,
                Repeat = pd.Repeat,
                Sig = pd.Sig

               
            }));

            return Ok(prescriptionDrugDtos);

        }

      




        // list all drugs in a prescription
        //GET; api/PrescriptionDrugData/ListDrugsForPrescription/5
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        public IHttpActionResult ListDrugsForPrescription(int id)
        {
            List<PrescriptionDrug> prescriptionDrugs = db.PrescriptionsDrugs.Where(pd => pd.PrescriptionID == id).ToList();
            List<PrescriptionDrugDto> prescriptionDrugDtos = new List<PrescriptionDrugDto>();

            prescriptionDrugs.ForEach(pd => prescriptionDrugDtos.Add(new PrescriptionDrugDto()
            {
                PrescriptionID = pd.PrescriptionID,
                DrugId = pd.DrugID,
                DoctorName = pd.Prescription.DoctorName,
                PatientName = pd.Prescription.FirstName + ' ' + pd.Prescription.LastName,
                DrugName = pd.Drug.DrugName,
                Quantity = pd.Quantity,
                Repeat = pd.Repeat,
                Sig = pd.Sig
            }));
            return Ok(prescriptionDrugDtos);

        }

        // list all drugs in a prescription
        //GET; api/PrescriptionDrugData/ListDrugsForPrescription/5
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        public IHttpActionResult FindDrugFromPrescription(int prescriptionId, int DrugId)
        {
            Debug.WriteLine("in FindDrugFromPrescription data controleer with " + prescriptionId + " " + DrugId);
            List<PrescriptionDrug> prescriptionDrugs = db.PrescriptionsDrugs.Where(pd => pd.PrescriptionID == prescriptionId && pd.DrugID == DrugId).ToList();
            List<PrescriptionDrugDto> prescriptionDrugDtos = new List<PrescriptionDrugDto>();
            Debug.WriteLine("COUNT-------" + prescriptionDrugs.Count());
            prescriptionDrugs.ForEach(pd => prescriptionDrugDtos.Add(new PrescriptionDrugDto()
            {
          
                PrescriptionID = pd.PrescriptionID,
                DrugId = pd.DrugID,
                DoctorName = pd.Prescription.DoctorName,
                PatientName = pd.Prescription.FirstName + ' ' + pd.Prescription.LastName,
                DrugName = pd.Drug.DrugName,
                Quantity = pd.Quantity,
                Repeat = pd.Repeat,
                Sig = pd.Sig
            }));
            return Ok(prescriptionDrugDtos);

        }

        // find a drug matching a drug id in the prescription mation prescription id
        //GET; api/PrescriptionDrugData/FindPrescriptionDrug/5/8
        [HttpGet]
        [ResponseType(typeof(PrescriptionDrugDto))]
        //[Route("api/PrescriptionDrugData/FindPrescriptionDrug/{prescriptionId}/{drugId}")]
        public IHttpActionResult FindPrescriptionDrugOld(int prescriptionId, int drugId)
        {
            PrescriptionDrug prescriptionDrug = db.PrescriptionsDrugs.Where(pd => pd.PrescriptionID == prescriptionId && pd.DrugID == drugId).FirstOrDefault();
            if (prescriptionDrug == null )
            {
                return NotFound();
            }
            
            PrescriptionDrugDto pdDto = new PrescriptionDrugDto()
            {
                PrescriptionID = prescriptionDrug.PrescriptionID,
                DoctorName = prescriptionDrug.Prescription.DoctorName,
                PatientName = prescriptionDrug.Prescription.FirstName + ' ' + prescriptionDrug.Prescription.LastName,
                DrugName = prescriptionDrug.Drug.DrugName,
                Quantity = prescriptionDrug.Quantity,
                Repeat = prescriptionDrug.Repeat,
                Sig = prescriptionDrug.Sig
            };
           

            return Ok(pdDto);

/*
            Debug.WriteLine("---eneterd data controller----");
            List<PrescriptionDrug> prescriptionDrugs = db.PrescriptionsDrugs.Where(pd => pd.PrescriptionID == prescriptionId && pd.DrugID == drugId).ToList();
            List<PrescriptionDrugDto> prescriptionDrugDtos = new List<PrescriptionDrugDto>();

            prescriptionDrugs.ForEach(pd => prescriptionDrugDtos.Add(new PrescriptionDrugDto()
            {
                PrescriptionID = pd.PrescriptionID,
                DoctorName = pd.Prescription.DoctorName,
                PatientName = pd.Prescription.FirstName + ' ' + pd.Prescription.LastName,
                DrugName = pd.Drug.DrugName,
                Quantity = pd.Quantity,
                Repeat = pd.Repeat,
                Sig =  pd.Sig
            }));
            Debug.WriteLine("---returning DTO----");
            return Ok(prescriptionDrugDtos);*/
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

        //POST : api/PrescriptionDrugData/AddPrescriptionDrug
        [ResponseType(typeof(PrescriptionDrug))]
        [HttpPost]
        public IHttpActionResult AddPrescriptionDrug(PrescriptionDrug prescriptionDrug)
        {
            Debug.WriteLine(" Enter DataController for Add ");
            Debug.WriteLine(prescriptionDrug);
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Bad Request---");
                return BadRequest(ModelState);
            }

            //prescriptionDrug.PrescriptionID =  .CreatedDate = DateTime.Today;
            //prescription.Status = true;
            db.PrescriptionsDrugs.Add(prescriptionDrug);
            db.SaveChanges();

            Debug.WriteLine(" saved with id---- " + prescriptionDrug.PrescriptionID);

            //return Ok(prescriptionDrug);
           
            return CreatedAtRoute("DefaultApi", new { id = prescriptionDrug.PrescriptionID }, prescriptionDrug);

        }




        [HttpPost]
        [Route("api/PrescriptionDrugData/UnAssociateDrug/{prescriptionid}/{drugid}")]
        public IHttpActionResult UnAssociateDrug(int prescriptionid, int drugid)
        {
            Debug.WriteLine(" in data controller of dele");
            PrescriptionDrug SelectedPrescription = db.PrescriptionsDrugs.Where(p =>p.PrescriptionID == prescriptionid && p.DrugID == drugid).FirstOrDefault();
            //Drug SelectedDrug = db.Drugs.Find(drugid);

            if (SelectedPrescription == null )
            {
                return NotFound();
            }

            Debug.WriteLine("input presc id is: " + prescriptionid);
            Debug.WriteLine("selected presc id is: " + SelectedPrescription.PrescriptionID);
            Debug.WriteLine("input drug id is: " + drugid);
            Debug.WriteLine("selected drug name is: " + SelectedPrescription.DrugID);

            db.PrescriptionsDrugs.Remove(SelectedPrescription);
            db.SaveChanges();
            return Ok();
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

        // DELETE: api/PrescriptionDrugData/DeletePrescriptionDrug/5
        [HttpPost]
        [Route("api/PrescriptionDrugData/DeletePrescriptionDrug/{prescriptionid}/{drugid}")]


       // [ResponseType(typeof(PrescriptionDrug))]
        public IHttpActionResult DeletePrescriptionDrug(int prescriptionid, int drugid)
        {
            PrescriptionDrug prescriptionDrug = db.PrescriptionsDrugs.Where(pd=>pd.PrescriptionID == prescriptionid && pd.DrugID == drugid).FirstOrDefault() ;
            if (prescriptionDrug == null)
            {
                return NotFound();
            }

            Debug.WriteLine("input presc id is: " + prescriptionid);
            Debug.WriteLine("selected qty : " + prescriptionDrug.Quantity);
 


            db.PrescriptionsDrugs.Remove(prescriptionDrug);
            db.SaveChanges();

            return Ok();
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