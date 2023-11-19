using Hospital_App.Models;
using System.Collections.Generic;

namespace Hospital_App.CollectionViewModel
{
    public class SuburbCollection
    {
        public Suburb Suburb { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
