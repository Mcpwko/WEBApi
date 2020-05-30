using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }
    }
}
