using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingDSChallenge.Model
{
    public class CustomerTotalAmount : IComparable<CustomerTotalAmount>
    {
        public string _customerName { get; }
        public decimal _totalAmount { get; }

        public CustomerTotalAmount(string customerName, decimal totalAmount)
        {
            _customerName = customerName;
            _totalAmount = totalAmount;
        }
        public int CompareTo(CustomerTotalAmount other)
        {
            return _totalAmount.CompareTo(other._totalAmount);
        }
    }
}
