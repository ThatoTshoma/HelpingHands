using Hospital_App.Models;
using System.Collections.Generic;

namespace Hospital_App.CollectionViewModel
{
    public class ScheduleCollection
    {
        public Schedule Schedule { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
        public IEnumerable<Nurse> Nurses { get; set; }
    }
}
