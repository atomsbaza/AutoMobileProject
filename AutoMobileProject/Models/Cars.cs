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
    public class Cars
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Vehicle number")]
        public string Vin { get; set; }

        [Required]
        [DisplayName("Brand")]
        public string Make { get; set; }

        [Required]
        [DisplayName("Model")]
        public string Model { get; set; }

        [Required]
        public string Style { get; set; }

        [Required]
        public int? Year { get; set; }

        [Required]
        public double? Miles { get; set; }
        public string Color { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
