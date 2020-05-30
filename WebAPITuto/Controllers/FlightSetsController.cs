using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightSetsController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public FlightSetsController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/FlightSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightSet>>> GetFlightSet()
        {
            return await _context.FlightSet.ToListAsync();
        }

        [HttpGet]
        [Route("available")]
        public async Task<ActionResult<IEnumerable<FlightSet>>> GetAvailableFlights()
        
        {
            var flights = await _context.FlightSet.ToListAsync();
            IEnumerable<FlightSet> available = flights.Where(x => (x.Date >= DateTime.Today) && ( x.Seats > 0));
            available.Select(c => { c.Price = PriceCalculation(c.Price,c.Date,c.Seats,c.BookingSet.Count); return c; }).ToList();
            var flightsavailable = available.ToList();


            return flightsavailable;
        }

        private double PriceCalculation(double price, DateTime day, int totalSeats, int seatsBooked)
        {
            double percent = seatsBooked / totalSeats;
            double monthsleft = day.Month - DateTime.Today.Month;
            Console.WriteLine(percent + " " + monthsleft);

            if (percent > 0.8)
            {
                price = price * 150 / 100;
            }
            else
            {
                if(percent < 0.2 && monthsleft < 2)
                {
                    price = price * 80 / 100;
                }
                else
                {
                    if(percent <0.5 && monthsleft < 1){
                        price = price * 70 / 100;
                    }
                }
            }
            

            return price;
        }

        // GET: api/FlightSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightSet>> GetFlightSet(int id)
        {
            var flightSet = await _context.FlightSet.FindAsync(id);

            if (flightSet == null)
            {
                return NotFound();
            }

            return flightSet;
        }

        // GET: api/FlightSets/5/price
        [HttpGet("{id}/price")]
        public async Task<ActionResult<double>> GetFlightSetSalesPrice(int id)
        {
            var flightSet = await _context.FlightSet.FindAsync(id);

            if (flightSet == null)
            {
                return NotFound();
            }
            
            var price = PriceCalculation(flightSet.Price, flightSet.Date, flightSet.Seats, flightSet.BookingSet.Count);

            return price;
        }

        // GET: api/FlightSets/5/sales
        [HttpGet("{id}/sales")]
        public async Task<ActionResult<double>> GetFlightTotalSales(int id)
        {
            var bookingsets = await _context.BookingSet.ToListAsync();

            var price = bookingsets.Where(x=> x.FlightNo == id).Sum(c => c.SalePrice);

            return price;
        }

        // GET: api/FlightSets/sales/LAX
        [HttpGet("sales/{destination}")]
        public async Task<ActionResult<double>> GetAveragePriceFromDestination(string destination)
        {
            var bookingsets = await _context.BookingSet.ToListAsync();

            var price = bookingsets.Where(x => x.Flight.Destination == destination).Average(x => x.SalePrice);
            return price;
        }

        // PUT: api/FlightSets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlightSet(int id, FlightSet flightSet)
        {
            if (id != flightSet.FlightNo)
            {
                return BadRequest();
            }

            _context.Entry(flightSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightSetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FlightSets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FlightSet>> PostFlightSet(FlightSet flightSet)
        {
            _context.FlightSet.Add(flightSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlightSet", new { id = flightSet.FlightNo }, flightSet);
        }

        // DELETE: api/FlightSets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FlightSet>> DeleteFlightSet(int id)
        {
            var flightSet = await _context.FlightSet.FindAsync(id);
            if (flightSet == null)
            {
                return NotFound();
            }

            _context.FlightSet.Remove(flightSet);
            await _context.SaveChangesAsync();

            return flightSet;
        }

        private bool FlightSetExists(int id)
        {
            return _context.FlightSet.Any(e => e.FlightNo == id);
        }
    }
}
