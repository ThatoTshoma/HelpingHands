using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_App.Models
{
    public class City
    {
        public int CityID { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name = "City Abbreviation")]
        public string CityAbbreviation { get; set; }
        

    }


}
