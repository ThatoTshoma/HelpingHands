using Hospital_App.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_App.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public ApplicationUser ApplicationyUser { get; set; }
        public int ApplicationUserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string Surname { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email AddressLine1")]
        public string EmailAddress { get; set; }
    }
}
