using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Detail")]
        public string Details { get; set; }

        [DisplayName("Date add")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateAdded { get; set; }

        public int CarId { get; set; }

        [DisplayName("Service")]
        public int ServiceTypeId { get; set; }

        [ForeignKey("CarId")]
        public virtual Cars Cars { get; set; }

        
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceTypes ServiceTypes { get; set; }
    }
}
