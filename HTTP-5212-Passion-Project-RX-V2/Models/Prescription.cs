using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP_5212_Passion_Project_RX_V2.Models
{
    public class Prescription
    {

        [Key]
        public int ID { get; set; } //Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DoctorName { get; set; }

        public DateTime CreatedDate { get; set; }
        public Boolean Status { get; set; } // to store true / false

        //@todo can store more details of patient like address etc


        // RelationShip : Many to Many Relationship between Prescription and Drug
        // In words, A Prescription can have Multiple Drugs and multiple prescriptions can have one/same drug
        public virtual ICollection<PrescriptionDrug> Drugs { get; set; }

        // IMP?? OR should it be
        //   public virtual ICollection<Drug> Drugs { get; set; }

    }

    public class PrescriptionDto
    {
        public int ID { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }

        public string DrugName { get; set; }
        public int Qty { get; set; } // PrescriptionDrug Table



    }
}