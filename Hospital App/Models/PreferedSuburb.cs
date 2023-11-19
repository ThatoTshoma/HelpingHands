using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_App.Models
{
    public class PreferedSuburb
    {
        public int PreferedSuburbID { get; set; }
        public Nurse Nurse { get; set; }

        [Display(Name = "Nurse Name")]
        public int NurseID { get; set; }
        public Suburb Suburb { get; set; }

        [Required]
        [Display(Name = "Suburb Name")]
        public int SuburbID { get; set;}

        [NotMapped]
        public List<int> SelectedSuburbs { get; set; }

    }

}
