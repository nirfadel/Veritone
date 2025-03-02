using CRMBilling.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Services
{
    public interface ICRMBillingService
    {
        Task<List<BillingRecord>> GetBillingRecords(CRMFilter crmFilter);

        Task CreateBillingRecord(CreateBillingRecordDto billingRecordDto);
    }
}
