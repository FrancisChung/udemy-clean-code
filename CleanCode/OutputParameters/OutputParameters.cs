using CleanCode.Comments;
using System;
using System.Collections.Generic;

namespace CleanCode.OutputParameters
{
    public class GetCustomersResult
    {
        private IEnumerable<Customer> _customers;
        private int _totalCount;

        public GetCustomersResult(IEnumerable<Customer> customers, int totalCount)
        {
            _totalCount = totalCount;
            _customers = customers;
        }

        public IEnumerable<Customer> Customers => _customers;
        public int Count => _totalCount;

    }

    public class OutputParameters
    {
        public void DisplayCustomers()
        {
            int totalCount = 0;
            const int pageIndex = 1;
            var tuple = GetCustomers(pageIndex);
            totalCount = tuple.Count;

            Console.WriteLine("Total customers: " + totalCount);
            foreach (var c in tuple.Customers)
                Console.WriteLine(c);
        }

        public GetCustomersResult GetCustomers(int pageIndex)
        {
            var totalCount = 100;
            return new GetCustomersResult(new List<Customer>(), totalCount);
        }
    }
}
