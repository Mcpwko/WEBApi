using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PilotSet:Employee
    {

        public int? FlightHours { get; set; }

        public virtual ICollection<FlightSet> FlightSet { get; set; }
    }
}
