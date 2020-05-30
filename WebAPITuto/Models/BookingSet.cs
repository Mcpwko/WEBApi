using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class BookingSet
    {
        public int FlightNo { get; set; }
        public int PassengerID { get; set; }
        public double SalePrice { get; set; }

        public virtual FlightSet Flight { get; set; }
        public virtual PassengerSet Passenger { get; set; }
    }
}
