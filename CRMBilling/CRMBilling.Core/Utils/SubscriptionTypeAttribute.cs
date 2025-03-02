using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRMBilling.Core.Utils.Enums;

namespace CRMBilling.Core.Utils
{
    public class BillingRecordStatusAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;
            return Enum.GetNames(typeof(BRStatus)).Contains(value);
        }
    }
}
