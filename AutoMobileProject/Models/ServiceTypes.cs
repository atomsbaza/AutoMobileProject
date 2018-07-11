using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMobileProject.Models
{
    public class ServiceTypes
    {
        public int Id { get; set; }

        [DisplayName("Service")]
        public string ServiceName { get; set; }

        [DisplayName("Create by")]
        public string CreateBy { get; set; }

        [DisplayName("Create time")]
        public DateTime CreateTime { get; set; }

        [DisplayName("Last updatt by")]
        public string LastUpdateBy { get; set; }

        [DisplayName("Last update time")]
        public DateTime LastUpdateTime { get; set; }
    }
}
