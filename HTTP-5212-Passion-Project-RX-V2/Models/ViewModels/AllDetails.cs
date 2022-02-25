using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP_5212_Passion_Project_RX_V2.Models.ViewModels
{
    public class AllDetails
    {
        public IEnumerable<PrescriptionDto> allPrescription { get; set; }
        public IEnumerable<PrescriptionDrugDto> allPrescriptionDrug { get; set; }
       
    }
}