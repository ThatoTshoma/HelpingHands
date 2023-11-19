using Hospital_App.Models;
using System.Collections;
using System.Collections.Generic;

namespace Hospital_App.CollectionViewModel
{
    public class PreferredSuburbCollection
    {
        public PreferedSuburb PreferedSuburb { get; set; }
        public IEnumerable<Suburb> Suburb { get; set; }

    }
}
