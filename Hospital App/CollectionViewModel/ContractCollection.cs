using Hospital_App.Models;
using System.Collections.Generic;
using System.Numerics;

namespace Hospital_App.CollectionViewModel
{
    public class ContractCollection
    {
        public Contract Contract { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Nurse> Nurses { get; set; }
        public IEnumerable<Suburb>Suburbs { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Suburb> Suburb { get; set; }
    }
}
