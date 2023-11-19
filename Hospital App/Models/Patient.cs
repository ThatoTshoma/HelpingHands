using Hospital_App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_App.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ApplicationUserId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Fist Name")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Surname { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Patient Name")]

        public string FullName { get; set; }

        [Column(TypeName = "nvarchar(13)")]
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Emergenct Number")]

        public string EmergencyNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Emergenct Contact Person")]
        public string EmergencyContatctPerson { get; set; }

        public string Gender { get; set; }
        [Display(Name = "Date Of Birth")]

        public DateTime DateOfBirth { get; set; }


        public List<PatientChronic> PatientChronics { get; set; }

    }


}
