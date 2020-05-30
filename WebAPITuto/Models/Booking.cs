using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Booking
    {
        public string Name { get; set; }
        public string Firstname { get; set; }
        public int FlightNo { get; set; }
        public double SalesPrice { get; set; }
    }
}
