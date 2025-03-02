using CRMBilling.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Repository
{
    public interface IRepository
    {
        Task<List<BillingRecord>> GetBillingRecords(CRMFilter crmFilter);

        Task SaveNewBillingRecord(BillingRecord billingRecord);
    }
}
