using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRMBilling.Core.Model
{
    public class BillingRecord
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Key]
        public int Id { get; init; }
        public string CustomerName { get; init; }
        public string SubscriptionType { get; init; }
        public decimal BillingAmount { get; init; }
        public DateTime? BillingDate { get; init; }
        public string Status { get; init; }
        public DateTime? NextBillingDate { get; init; }
    }
}
