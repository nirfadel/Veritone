using CRMBilling.Core.Model;
using CRMBilling.Core.Repository;
using CRMBilling.Core.Utils;
using CRMBilling.Core.Utils.ErrorHandling;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Services
{
    public class CRMBillingService : ICRMBillingService
    {
        private readonly IErrorHandler _errorHandler;
        private readonly ILogger<CRMBillingService> _logger;
        private readonly IRepository _repository;
        public CRMBillingService(IErrorHandler errorHandler, ILogger<CRMBillingService> logger,
            IRepository repository)
        {
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task CreateBillingRecord(CreateBillingRecordDto billingRecordDto)
        {
            try
            {
               
                var billingRecord = MapperConfig.Map(billingRecordDto);
                await _repository.SaveNewBillingRecord(billingRecord);
                _logger.LogInformation("Billing record saved!");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<BillingRecord>> GetBillingRecords(CRMFilter crmFilter)
        {
            var billingRecords = new List<BillingRecord>();
            try
            {
                billingRecords = await _repository.GetBillingRecords(crmFilter);
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex);
            }
            return billingRecords;
        }
    }
}
