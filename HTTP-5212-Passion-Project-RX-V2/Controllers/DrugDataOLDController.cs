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
    public class DrugDataOLDController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all the available drugs in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all drugs in the database
        /// <example>
        /// GET: api/DrugData/Listalldrugs/
        /// </example>
        /// </returns>
       // [Route("api/DrugData/ListAllDrugs")]
        [HttpGet]
        public IQueryable<DrugDto> ListAllDrug()
        {
            var drugs = from d in db.Drugs
                        select new DrugDto()
                        {
                            DrugID = d.ID,
                            DrugName = d.DrugName,
                            Dosage = d.Dosage,
                            Formulation = d.Formulation.ToString()
                        };
                        return drugs;
        }

        /* public IQueryable<Drug> ListDrugs()
         {
             return db.Drugs
                 .Include(d=>d.Prescriptions);
         }*/

        /// <summary>
        /// Returns all the available drugs in the system along with count of all prescriptions that containd drug
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all drugs in the database
        /// <example>
        /// GET: api/DrugData/Listalldrugs/
        /// </example>
        /// </returns>
        [Route("api/DrugData/ListAllDrugsWithPrescriptionCount")]
        [HttpGet]
        public IQueryable<DrugDto> ListAllDrugsWithPrescriptionCount()
        {
            var drugs = from d in db.Drugs.Include(d => d.Prescriptions)
                        select new DrugDto()
                        {
                            DrugID = d.ID,
                            DrugName = d.DrugName,
                            Dosage = d.Dosage,
                            Formulation = d.Formulation.ToString(),
                            PrescriptionCount = d.Prescriptions.Count
                        };
            return drugs;
        }

        /// <summary>
        /// Returns all the available drugs in the system matching a given prescription ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all drugs in the database
        /// <example>
        /// GET: api/DrugData/Listalldrugs/
        /// </example>
        /// </returns>
        [Route("api/DrugData/ListAllDrugsForPrescription/pid")]
       // [HttpGet]
        /*[ResponseType(typeof(DrugDto))]
        public IHttpActionResult ListAllDrugsForPrescription(int pid)
        {
            List<Drug> Drugs = db.Drugs.Where(d=>d.Prescriptions.Any(p=>p.ID==pid)).ToList();
            List<DrugDto> DrugDtos = new List<DrugDto>();

            Drugs.ForEach(d => DrugDtos.Add(new DrugDto()
            {
                DrugName = d.DrugName,
                Dosage = d.Dosage,
                Formulation = d.Formulation.ToString(),
             
                //PrescriptionCount = d.Prescriptions.Count, // hoe many pt=recriptions contain this drug
            }));

            return Ok(DrugDtos);
        }*/

        /// <summary>
        /// Returns drug details of a drug matching the given id
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A drug in the system matching up the drugID which is Primary Key
        /// </returns>
        /// <param name="drugID">Primary key of the drug</param>
        /// <example>
        /// GET: /api/drugdata/FindDrug/2
        /// </example>   
        [Route("api/DrugData/FindDrug/{id}")]
        [ResponseType(typeof(Drug))]
        [HttpGet]
        public IHttpActionResult FindDrug(int id)
        {
            Drug drug = db.Drugs.Find(id);
            if (drug == null)
            {
                return NotFound();
            }

            return Ok(drug);
        }

        /// <summary>
        /// Updates a particular drug in the system with POST Data Input
        /// </summary>
        /// <param name="id">Represents the drugID Primary Key</param>
        /// <param name="drug">JSON Form Data of a Drug including id of drug to be updated</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response) or
        /// HEADER: 400 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DrugData/UpdateDrug/5
        /// Drug Json Object
        /// </example>
       /* [Route("api/DrugData/UpdateDrug/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDrug(int id, Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drug.ID)
            {
                return BadRequest();
            }

            db.Entry(drug).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrugExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/

        /// <summary>
        /// Add a new drug to the system
        /// </summary>
        /// <param name="drug">JSON form data of a new drug (no id)</param> 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Drug Id, Drug Data
        /// or
        /// HEADER: 404 (Bad request)
        /// </returns>
        /// <example>
        /// POST: api/DrugData/AddDrug
        /// FORM DATA: Drug json object
        /// </example>
        // POST: api/DrugData/AddDrug
       /* [ResponseType(typeof(Drug))]
        [HttpPost]
        public IHttpActionResult AddDrug(Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drugs.Add(drug);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drug.ID }, drug);
        }*/

        /// <summary>
        /// Deletes a Drug from the system matching to the given Drug Id
        /// </summary>
        /// <param name="id">The primary key of the Drug</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///  DELETE: api/DrugData/DeleteDrug/5
        /// </example>
        /*[Route("api/DrugData/DeleteDrug/{id}")]
        // DELETE: api/DrugData/5
        [ResponseType(typeof(Drug))]
        public IHttpActionResult DeleteDrug(int id)
        {
            Drug drug = db.Drugs.Find(id);
            if (drug == null)
            {
                return NotFound();
            }

            db.Drugs.Remove(drug);
            db.SaveChanges();

            return Ok(drug);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrugExists(int id)
        {
            return db.Drugs.Count(e => e.ID == id) > 0;
        }
    }
}