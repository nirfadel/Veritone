using CRMBilling.Core.DB;
using CRMBilling.Core.Model;
using CRMBilling.Core.Services;
using CRMBilling.Core.Utils.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Repository
{
    public class Repository : IRepository
    {
        private readonly CRMBillingDBContext _DBContext;
        private readonly IErrorHandler _errorHandler;
        private readonly ILogger<Repository> _logger;
        private readonly IDistributedCache _cache;
        const string cachedKey = "billing_records";
        public Repository(CRMBillingDBContext dBContext, IErrorHandler errorHandler,
            ILogger<Repository> logger, IDistributedCache cache)
        {
            _DBContext = dBContext;
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache;
        }
        public async Task<List<BillingRecord>> GetBillingRecords(CRMFilter crmFilter)
        {
            return await _errorHandler.ExecuteWithErrorHandlingAsync(async () =>
            {
                List<BillingRecord> billingRecords = new List<BillingRecord>();
                IQueryable<BillingRecord>? query = null;
                bool isCached = true;
                query = await GetCachedData();
                if (query == null || !query.Any())
                {
                    query = _DBContext.BillingRecords;
                    var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    string cachedData = JsonConvert.SerializeObject(query);
                    await _cache.SetStringAsync(cachedKey, cachedData, options);
                    isCached = false;
                }

                if (crmFilter.status != "all")
                {
                    query = query.Where(x => x.Status == crmFilter.status);
                }
                if (crmFilter.startDate != null && crmFilter.endDate != null)
                {
                    query = query.Where(d => d.BillingDate >= crmFilter.startDate && d.BillingDate <= crmFilter.endDate);
                }
                if (query != null)
                {
                    billingRecords = isCached ? query.ToList() : await query.ToListAsync();
                    
                }
                   
                return billingRecords;
            });
        }

        public async Task SaveNewBillingRecord(BillingRecord billingRecord)
        {
            try
            {
                await _errorHandler.ExecuteWithErrorHandlingAsync(async () =>
                {
                    _DBContext.BillingRecords.Add(billingRecord);
                    await _DBContext.SaveChangesAsync();
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IQueryable<BillingRecord>> GetCachedData()
        {
            List<BillingRecord> billingRecords = new List<BillingRecord>();
            var cachedData = await _cache.GetStringAsync(cachedKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                billingRecords = JsonConvert.DeserializeObject<List<BillingRecord>>(cachedData);
            }
            return billingRecords.AsQueryable<BillingRecord>();
        }
    }
}
