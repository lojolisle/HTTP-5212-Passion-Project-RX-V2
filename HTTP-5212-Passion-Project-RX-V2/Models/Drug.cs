using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP_5212_Passion_Project_RX_V2.Models
{
    public enum Formulation
    {
        Tablet, Capsule, Granules, Powder, Liquid, Cream, Oinment
    }
    public class Drug
    {
        [Key]
        public int ID { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }

        // Formulatioin specifies physical form of drug eg: capsules, tablets
        public Formulation? Formulation { get; set; } // this property is enum and the question mark declaration indicates that the Formulation property is nullable


        // RelationShip : Many to Many Relationship between Drug and Prescription
       

        // A Drug entity can be related to any Number of Precription Entities
        public virtual ICollection<PrescriptionDrug> Prescriptions { get; set; }

        // IMP?? OR should it be
        //   public virtual ICollection<Prescription> Prescriptions { get; set; }
    }

    public class DrugDto
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }
        public string Formulation { get; set; }
        public int PrescriptionCount { get; set; }

      
    }
}