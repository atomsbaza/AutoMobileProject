using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMobileProject.Models.ViewModels
{
    public class CarAndServicesViewModel
    {
        // Car
        public Cars Cars { get; set; }
        public int carId { get; set; }
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Style { get; set; }
        public int? Year { get; set; }

        //Service
        public Service NewService { get; set; }
        public IEnumerable<Service> PastServices { get; set; }
        public List<ServiceTypes> ServiceTypeses { get; set; }
    }
}
