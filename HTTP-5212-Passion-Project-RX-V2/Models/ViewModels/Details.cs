using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP_5212_Passion_Project_RX_V2.Models.ViewModels
{
    public class Details
    {
        // this vm is a class that will show all drugs of a presc
        public PrescriptionDto selectedPrescription { get; set; }
        public IEnumerable<PrescriptionDrugDto> selectedPrescriptionDrug { get; set; }

        // allow for adding a new drug to this prescription

        public IEnumerable<PrescriptionDto> DoctorNames { get; set; }
        public IEnumerable<PrescriptionDto> PrescriptionDetails { get; set; } // includes ID and patient NAme
        public IEnumerable<DrugDto> DrugDetails { get; set; }
  


    }
}