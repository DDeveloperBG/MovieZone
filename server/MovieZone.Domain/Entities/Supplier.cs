namespace MovieZone.Domain.Entities
{
    using System.Collections.Generic;

    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }

        public List<Product> Products { get; set; }
    }
}
