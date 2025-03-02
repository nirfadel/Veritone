using CRMBilling.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Model
{
    public class CreateBillingRecordDto
    {
        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 50 characters")]
        public string CustomerName { get; init; }
        [SubscriptionType(ErrorMessage = "This Subscription Type is not valid")]
        [Required(ErrorMessage = "Subscription type is required")]
        public string SubscriptionType { get; init; }
        [Required(ErrorMessage = "Billing Record must have amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Billing amount must be greater than 0")]
        public decimal BillingAmount { get; init; }
        public DateTime? BillingDate { get; init; }
        
        [Required(ErrorMessage = "Billing record status is required")]
        [BillingRecordStatus(ErrorMessage = "This status is not valid")]
        public string Status { get; init; }
    }
}
