using CRMBilling.Core.Model;
using CRMBilling.Core.Services;
using CRMBilling.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CRMBilling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingsController : ControllerBase
    {
        private readonly ICRMBillingService _billingService;
        public BillingsController(ICRMBillingService billingService)
        {
            _billingService = billingService;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<BillingRecord>>>> Get([FromQuery] CRMFilter crmFilter)
        {
            try
            {
                List<BillingRecord> billingRecords = await _billingService.GetBillingRecords(crmFilter);
                return Ok(billingRecords.ToApiResponse("Billing record created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<BillingRecord>.ErrorResponse(
                "Failed to getting billing records",
                new List<string> { ex.Message }
                 ));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBillingRecordDto billingRecordDto)
        {
            try
            {
                await _billingService.CreateBillingRecord(billingRecordDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<Task>.ErrorResponse(
               "Failed to create billing record",
               new List<string> { ex.Message }
                ));
            }
        }
    }
}
