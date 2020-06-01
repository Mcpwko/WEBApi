using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PassengerSet:Person
    {

        public int Weight { get; set; } = 8;

        public virtual ICollection<BookingSet> BookingSet { get; set; }
    }
}
