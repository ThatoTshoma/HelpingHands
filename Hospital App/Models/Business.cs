using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Hospital_App.Models
{
    public class Business
    {
        [Key] 
        public int OrganizationID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set;}

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "NPO Number")]
        public string NPONumber { get; set;}

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set;}

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Contact Number")]
        public string ConatactNumber { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get;set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Operating Hours")]
        public string OperatingHours { get; set; }
        public Suburb Suburb { get; set; }
        [Display(Name = "Suburb Name")]
        public int SuburbID { get; set; }


    }

}
