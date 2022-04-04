namespace MovieZone.Domain.Entities
{
    using System.Collections.Generic;

    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
