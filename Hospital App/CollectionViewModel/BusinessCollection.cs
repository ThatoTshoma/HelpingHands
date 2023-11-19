using Hospital_App.Models;
using System.Collections.Generic;

namespace Hospital_App.CollectionViewModel
{
    public class BusinessCollection
    {
        public Business Business { get; set; }
        public IEnumerable<Suburb> Suburbs { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
