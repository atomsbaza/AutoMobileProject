using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AutoMobileProject.Models
{
    public class Service
    {
        public int Id { get; set; }
        public double Miles { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateAdded { get; set; }

        public int CarId { get; set; }

        public int ServiceTypeId { get; set; }

        [ForeignKey("CarId")]
        public virtual Cars Cars { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceTypes ServiceTypes { get; set; }
    }
}
