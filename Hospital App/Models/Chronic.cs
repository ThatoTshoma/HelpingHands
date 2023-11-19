using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_App.Models
{
    public class Chronic
    {
        public int ChronicID { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Chronic Name")]
        public string ChronicName { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(250)")]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}
