using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMobileProject.Models.ViewModels
{
    public class CarAndServicesViewModel
    {
        public Cars Cars { get; set; }
        public Service NewService { get; set; }
        public IEnumerable<Service> PastServices { get; set; }
        public List<ServiceTypes> ServiceTypeses { get; set; }
    }
}
