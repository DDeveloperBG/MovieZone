namespace MovieZone.Domain.Entities
{
    using System.Collections.Generic;

    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.Orders = new List<Order>();
        }

        public string CustomerName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public List<Order> Orders { get; set; }
    }
}
