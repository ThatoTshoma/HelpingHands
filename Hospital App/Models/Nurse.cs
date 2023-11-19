using Hospital_App.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_App.Models
{
    public class Nurse
    {
        public int NurseID { get; set; }
        public ApplicationUser ApplicationyUser { get; set; }
        public int ApplicationUserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Fist Name")] 
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(13)")]
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        public string Gender { get; set; }

        public string Status { get; set; }


        public List<PreferedSuburb> PreferedSuburbs { get; set; }

       


    }

}
