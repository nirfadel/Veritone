using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingDSChallenge.Model
{
    public class BillingRecord
    {
        public int Id { get; init; }
        public string CustomerName { get; init; }
        public string SubscriptionType { get; init; }
        public decimal BillingAmount { get; init; }
        public DateTime BillingDate { get; init; }
        public string Status { get; init; }
        public DateTime? NextBillingDate { get; init; }
    }
}
