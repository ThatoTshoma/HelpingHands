using Hospital_App.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Hospital_App.Models
{
    public class OfficeManager
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationyUser { get; set; }
        public int ApplicationUserId { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(13)")]
        [Display(Name = "ID Number")]
        public int IDNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]

        public string EmailAddress { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        public string Status { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
