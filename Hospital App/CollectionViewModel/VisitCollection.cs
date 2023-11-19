using Hospital_App.Models;
using System.Collections;
using System.Collections.Generic;

namespace Hospital_App.CollectionViewModel
{
    public class VisitCollection
    {
        public Visit Visit { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
        public IEnumerable<Nurse> Nurses { get; set; }

    }
}
