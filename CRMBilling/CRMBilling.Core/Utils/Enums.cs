using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Utils
{
    public class Enums
    {
        public enum SubscriptionType
        {
            Basic = 1,
            Premium = 2,
            Enterprise = 3
        }

        public enum BRStatus {
            Paid = 1,
            Pending = 2,
            Overdue = 3
        }

        public enum ErrorSeverity
        {
            Low,
            Medium,
            High,
            Critical
        }
    }
}
