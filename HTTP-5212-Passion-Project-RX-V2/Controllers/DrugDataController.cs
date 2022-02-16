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
    public class DrugDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DrugData
        public IQueryable<DrugDto> GetDrugs()
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

        // GET: api/DrugData/5
        [ResponseType(typeof(Drug))]
        public IHttpActionResult GetDrug(int id)
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
        /// POST: api/DrugData/updateDrug/5
        /// Drug Json Object
        /// </example>
        [Route("api/DrugData/UpdateExistingDrug/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateExistingDrug(int id, Drug drug)
        {
            Debug.WriteLine("In function");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("BAd request");
                return BadRequest(ModelState);
            }
            Debug.WriteLine(" given Drug id " + id + " drug.id = "+ drug.ID);
            if (id != drug.ID)
            {
                Debug.WriteLine("No id ");
                return BadRequest();
            }

            db.Entry(drug).State = EntityState.Modified;
            Debug.WriteLine(" before try ");
            try
            {
                Debug.WriteLine(" saving--- ");
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                Debug.WriteLine("catch ");
                if (!DrugExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine(" status code ");
            return StatusCode(HttpStatusCode.NoContent);
        }

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
        /// POST: api/DrugData/addNewDrug
        /// FORM DATA: Drug json object
        /// </example>
        [ResponseType(typeof(Drug))]
        [HttpPost]
        public IHttpActionResult AddNewDrug(Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drugs.Add(drug);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drug.ID }, drug);
        }
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
        ///  DELETE: api/DrugData/deleteDrug/5
        /// </example>
        [Route("api/DrugData/deleteDrug/{id}")]
        [ResponseType(typeof(Drug))]
        [HttpPost]
        // DELETE: api/DrugData/5

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
        }








        // PUT: api/DrugData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrug(int id, Drug drug)
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
        }

        // POST: api/DrugData
        [ResponseType(typeof(Drug))]
        public IHttpActionResult PostDrug(Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drugs.Add(drug);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drug.ID }, drug);
        }

        // DELETE: api/DrugData/5
       /* [ResponseType(typeof(Drug))]
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