using Hospital_App.Models;
using System.Collections;
using System.Collections.Generic;

namespace Hospital_App.CollectionViewModel
{
    public class PatientChronicCollection
    {
        public PatientChronic PatientChronic { get; set; }


        public IEnumerable<Chronic> Chronics { get; set; } 
    }
}
