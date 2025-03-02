using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Model
{
    public class CRMFilter
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string status { get; set; }
    }
}
