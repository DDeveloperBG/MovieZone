namespace MovieZone.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
    }
}
