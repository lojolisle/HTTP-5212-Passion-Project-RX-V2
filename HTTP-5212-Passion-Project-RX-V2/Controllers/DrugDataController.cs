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

        /// <summary>
        /// Returns all Drugs in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Drugs in the database
        /// </returns>
        /// <example>
        /// </example>
        // GET: api/DrugData/ListDrugs
        [HttpGet]
        [ResponseType(typeof(DrugDto))]
        public IHttpActionResult ListDrugs()
        {
            List<Drug> Drugs = db.Drugs.ToList();
            List<DrugDto> DrugDtos = new List<DrugDto>();

            Drugs.ForEach(d => DrugDtos.Add(new DrugDto()
            {
                DrugID = d.ID,
                DrugName = d.DrugName,
                Dosage = d.Dosage,
                Formulation = d.Formulation.ToString()

            }));

            return Ok(DrugDtos);
        }

        // GET: api/DrugData/GetAllDrugs
        [ResponseType(typeof(DrugDto))]
        [HttpGet]
        public IHttpActionResult GetAllDrugs()
        {

            List<Drug> drugs = db.Drugs.ToList();
            List<DrugDto> drugDetails = new List<DrugDto>();


            drugs.ForEach(d => drugDetails.Add(new DrugDto()
            {
                DrugID = d.ID,
                DrugName = d.DrugName
            }));

            return Ok(drugDetails);

        }

        /// <summary>
        /// Returns a Drug mapping givrn ID in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A drug in the system matching up to the Drug ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Drug</param>
        /// <example>
        /// GET: api/DrugData/FindDrug/5
        /// </example>
        [ResponseType(typeof(DrugDto))]
        [HttpGet]
        public IHttpActionResult FindDrug(int id)
        {
            Drug Drug = db.Drugs.Find(id);
            DrugDto DrugDto = new DrugDto()
            {
                DrugID = Drug.ID,
                DrugName = Drug.DrugName,
                Dosage = Drug.Dosage,
                Formulation = Drug.Formulation.ToString(),
                FormulationId = (int)Drug.Formulation
                
            };
            if (Drug == null)
            {
                return NotFound();
            }

            return Ok(DrugDto);
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
        [Route("api/DrugData/UpdateDrug/{id}")]
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