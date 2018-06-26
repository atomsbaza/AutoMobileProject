using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMobileProject.Models
{
    public class Cars
    {
        public int Id { get; set; }
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Style { get; set; }
        public int? Year { get; set; }
        public double? Miles { get; set; }
        public string Color { get; set; }
    }
}
