using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_App.Models
{
    public class Suburb
    { 
        public int SuburbID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Suburb Name")]
        public string SuburbName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(5)")]
        [Display(Name = "Postal Code")]
        public int PostalCode { get; set; }
        public City City { get; set; }

        [Required]
        [Display(Name = "City Name")]
        public int CityID { get; set; }
    }


}
