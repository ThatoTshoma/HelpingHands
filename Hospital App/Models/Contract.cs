using Microsoft.AspNetCore.Authentication;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Hospital_App.Models
{
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Contract Date")]
        public DateTime ContractDate { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Wound Description")]
        public string WoundDesc { get; set; }


        [Column(TypeName = "nvarchar(10)")]
        public string Status { get; set; }
        public Nurse Nurse { get; set; }
        [Display(Name = "Nurse Name")]
        public int? NurseID { get; set; }
        public Patient Patient { get; set; }
        [Display(Name = "Patient Name")]
        public int PatientID { get; set; }
        public Suburb Suburb { get; set; }

        [Required]
        [Display(Name = "Suburb")]
        public int SuburbID { get; set; }

    }

}
