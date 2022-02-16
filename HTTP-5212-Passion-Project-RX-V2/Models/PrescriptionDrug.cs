using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP_5212_Passion_Project_RX_V2.Models
{
    public class PrescriptionDrug
    {
        [Key]
        [Column(Order = 1)]
        public int PrescriptionID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int DrugID { get; set; }

        public int Quantity { get; set; }

        public int Repeat { get; set; }

        //Sig represents direction on how to take medications
        public string Sig { get; set; }


        //RELATION SHIP: MANY TO MANY
        //1) a drug can be prescribed for multiple Patients
        //2) and a Patient can be precribed multiple Drugs


        // Foreign Keys
        // Navigation Property Drug
        // Prescription Entity is associated with one Drug Entity
        public virtual Drug Drug { get; set; }

      
        // Navigation Property Prescription
        // Prescription Entity is associated with one Patient Entity
        public virtual Prescription Prescription { get; set; }

        
    }

    public class PrescriptionDrugDto
    {
        public int PrescriptionID { get; set; }

        public string DoctorName { get; set; } //prescription table

        public string PatientName { get; set; } // prescription table

        public string DrugName { get; set; } //drug table

        public int Qty { get; set; }

        public int Repeat { get; set; }


    }
}