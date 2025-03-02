using AutoMapper;
using CRMBilling.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMBilling.Core.Utils
{
    public static class MapperConfig
    {
        public static BillingRecord Map(CreateBillingRecordDto billingRecordDto)
        {
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<CreateBillingRecordDto, BillingRecord>();
            });

            var mapper = mapperConfig.CreateMapper();
            return mapper.Map<BillingRecord>(billingRecordDto);
        }
    }
}
