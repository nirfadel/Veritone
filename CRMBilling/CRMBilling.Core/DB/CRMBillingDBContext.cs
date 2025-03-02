using CRMBilling.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection.Metadata;

namespace CRMBilling.Core.DB
{
    public class CRMBillingDBContext : DbContext
    {
        public CRMBillingDBContext(DbContextOptions options) : base(options) { }
        
        public DbSet<BillingRecord> BillingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<BillingRecord>()
              .Property(b => b.BillingDate)
              .ValueGeneratedOnAdd();
            base.OnModelCreating(model);
        }
    }
}
