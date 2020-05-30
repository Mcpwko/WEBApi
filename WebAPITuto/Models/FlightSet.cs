using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class FlightSet
    {
        public FlightSet() { }

        [Key]
        public int FlightNo { get; set; }
        [StringLength(50), MinLength(3)]
        public string Departure { get; set; }
        [StringLength(50), MinLength(3)]
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public short Seats { get; set; }
        public double Price { get; set; }

        [ForeignKey("PilotId")]
        public virtual PilotSet Pilot { get; set; }
        public int PilotId { get; set; }

        public virtual ICollection<BookingSet> BookingSet { get; set; }
    }
}
