using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMobileProject.Models
{
    public class IndividualButtonPartial
    {
        public string ButonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }

        public int? ServiceTypesId { get; set; }
        public string CustomerId { get; set; }
        public int? CarId { get; set; }
        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder(@"/");
                if (ServiceTypesId != 0 && ServiceTypesId != null)
                {
                    param.Append(String.Format("{0}", ServiceTypesId));
                }
                if (CustomerId != null)
                {
                    param.Append(String.Format("{0}", CustomerId));
                }
                if (CarId != 0 && CarId != null)
                {
                    param.Append(String.Format("{0}", CarId));
                }
                return param.ToString().Substring(0, param.Length);
            }
        }
    }
}
