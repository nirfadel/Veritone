using Microsoft.VisualStudio.TestTools.UnitTesting;
using BillingDSChallenge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingDSChallenge.Model;
using System.Diagnostics;

namespace BillingDSChallenge.Tests
{
    [TestClass()]
    public class TestingTopKCustomerTests
    {
        [TestMethod()]
        public void TestEmptyList()
        {
            var records = new List<BillingRecord>();
            var topK = TopKCustomers.FindTopKCustomers(records, 5);
            Assert.Equals(0, topK.Count);
        }

        [TestMethod()]
        public void TestNullList()
        {
            var topK = TopKCustomers.FindTopKCustomers(null, 5);
            Assert.Equals(0, topK.Count);
        }

        [TestMethod()]
        public void TestRegularList()
        {
            var records = new List<BillingRecord>
        {
            new BillingRecord { Id = 555, CustomerName = "Customer1", BillingAmount = 100.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 666, CustomerName = "Customer2", BillingAmount = 200.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 777, CustomerName = "Customer1", BillingAmount = 150.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 888, CustomerName = "Customer3", BillingAmount = 300.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 999, CustomerName = "Customer2", BillingAmount = 200.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 1111, CustomerName = "Customer4", BillingAmount = 150.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 2222, CustomerName = "Customer5", BillingAmount = 300.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 3333, CustomerName = "Customer4", BillingAmount = 50.0m, BillingDate = DateTime.Now }
        };

            var topK = TopKCustomers.FindTopKCustomers(records, 2);
        }

        [TestMethod()]
        public void TestMinusData()
        {
            var records = new List<BillingRecord>
        {
            new BillingRecord { Id = 555, CustomerName = "Customer1", BillingAmount = 100.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 666, CustomerName = "Customer2", BillingAmount = 200.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 777, CustomerName = "Customer1", BillingAmount = 150.0m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 888, CustomerName = "Customer3", BillingAmount = -300m, BillingDate = DateTime.Now },
            new BillingRecord { Id = 999, CustomerName = "Customer2", BillingAmount = 70.0m, BillingDate = DateTime.Now }
        };

            var topK = TopKCustomers.FindTopKCustomers(records, 3);
            CollectionAssert.AreEqual(new List<string> { "Customer2", "Customer1", "Customer3" }, topK);
        }

        [TestMethod()]
        public void TestScalability()
        {
            // Test with increasingly large datasets
            var sizes = new[] { 1000, 10000, 100000, 1000000 };
            int k = 10;
            var random = new Random(53);

            foreach (int size in sizes)
            {
                // Generate test data
                var records = new List<BillingRecord>(size);
                int customerCount = Math.Min(size / 10, 100000);  // 10% of records or max 100k customers

                for (int i = 0; i < size; i++)
                {
                    records.Add(new BillingRecord
                    {
                        Id = i,
                        CustomerName = $"Customer{random.Next(customerCount)}",
                        BillingAmount = (decimal)(random.NextDouble() * 1000),
                        BillingDate = DateTime.Now.AddDays(-random.Next(365))
                    });
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                var topK = TopKCustomers.FindTopKCustomers(records, k);
                sw.Stop();

                Console.WriteLine($"Records: {size}, Customers: ~{customerCount}, Time: {sw.ElapsedMilliseconds}ms");

                // Verify result size
                Assert.AreEqual(Math.Min(k, customerCount), topK.Count);
            }
        }
    }
}