using BillingDSChallenge.Model;
using BillingDSChallenge;
using NUnit.Framework;
using System.Diagnostics;

var records = new List<BillingRecord>
        {
            new BillingRecord { Id = 555, CustomerName = "Customer1", BillingAmount = 100.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 666, CustomerName = "Customer2", BillingAmount = 200.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 777, CustomerName = "Customer1", BillingAmount = 150.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 888, CustomerName = "Customer3", BillingAmount = 300.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 999, CustomerName = "Customer2", BillingAmount = 50.0m, BillingDate = DateTime.Now }
        };

var topK = TopKCustomers.FindTopKCustomers(records, 2);
