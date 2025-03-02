using BillingDSChallenge.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingDSChallenge
{
    public  class TopKCustomers
    {

   
        public static List<string> FindTopKCustomers(List<BillingRecord> billingRecords, int k)
        {
            if (billingRecords == null || !billingRecords.Any() || k <= 0)
            {
                return new List<string>();
            }

            // Step 1: Aggregate total amounts by customer
            Dictionary<string, decimal> customerTotals = new Dictionary<string, decimal>();

            foreach (var record in billingRecords)
            {
                if (customerTotals.ContainsKey(record.CustomerName))
                {
                    customerTotals[record.CustomerName] += record.BillingAmount;
                }
                else
                {
                    customerTotals[record.CustomerName] = record.BillingAmount;
                }
            }

            // Step 2: Use a min heap of size K to track top K customers
            PriorityQueue<string, decimal> minHeap = new PriorityQueue<string, decimal>(k);

            foreach (var entry in customerTotals)
            {
                string customerName = entry.Key;
                decimal totalAmount = entry.Value;
                string peekName = string.Empty;
                decimal peekAmount = 0;
                if (minHeap.Count < k)
                {
                    // Heap not full, add customer
                    minHeap.Enqueue(customerName, totalAmount);
                }
                else if (minHeap.TryPeek(out peekName, out peekAmount) && totalAmount > peekAmount)
                {
                    // If current customer's total is greater than the smallest in our heap
                    minHeap.Dequeue(); // Remove smallest
                    minHeap.Enqueue(customerName, totalAmount); // Add current customer
                }
            }

            // Step 3: Convert heap to list of customer names (in descending order of amount)
            List<string> topKCustomers = new List<string>(k);
            var tempStack = new Stack<string>();

            // Extract from heap (ascending order)
            while (minHeap.Count > 0)
            {
                tempStack.Push(minHeap.Dequeue());
            }

            // Pop from stack (gives descending order)
            while (tempStack.Count > 0)
            {
                topKCustomers.Add(tempStack.Pop());
            }

            return topKCustomers;
        }
    }
}
