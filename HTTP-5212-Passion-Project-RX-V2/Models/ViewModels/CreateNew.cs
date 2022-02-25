using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP_5212_Passion_Project_RX_V2.Models.ViewModels
{
    public class CreateNew
    {
        // doctorsname list , patientname list
        
        public IEnumerable<PrescriptionDto> DoctorNames { get; set; }
        public IEnumerable<PrescriptionDto> PrescriptionDetails { get; set; } // includes ID and patient NAme
        public IEnumerable<DrugDto> DrugDetails { get; set; }
        public IEnumerable<PrescriptionDrugDto> selectedPrescriptionDrug { get; set; }
    }
}