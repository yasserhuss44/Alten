using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Infrastructure.Entities
{
    public class Customer
    {
        public Customer()
        {

        }
        public Customer(string name,string address)
        {
            this.Name = name;
            this.Address = address;
        }
        public int Id { get; set; }

        public string Name { get;  set; }

        public string Address { get; set; }

      

    }
}
