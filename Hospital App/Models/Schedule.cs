using System.ComponentModel.DataAnnotations;
using System;

namespace Hospital_App.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Scedule Date")]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "Approximate Arrive Time")]
        public TimeSpan ApproxArriTime { get; set; }


        [Display(Name = "Depart Time")]
        public TimeSpan DepartTime { get; set; }

        [Display(Name = "Arrive Time")]

        public TimeSpan ArriveTime { get; set; }

        [Display(Name = "Wound Condition")]
        public string WoundCondition { get; set; }
        public string Note { get; set; }
        public Nurse Nurse { get; set; }

        [Display(Name = "Nurse Name")]

        public int NurseID { get; set; }
        public Contract Contract { get; set; }
        [Display(Name = "Contract Name")]

        public int ContractID { get; set; }
    }
}
