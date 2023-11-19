using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_App.Models
{
    public class PatientChronic
    {
        public int PatientChronicID { get; set; }
        public Patient Patient { get; set; }

        [Display(Name = "Patient Name")]
        public int PatientID { get; set;}
        public Chronic Chronic { get; set; }

        [Required]
        [Display(Name = "Chronic Condotion Name")]
        public int ChronicID { get; set; }

        [NotMapped]
        public List<int> SelectedChronics { get; set; }


    }


}
