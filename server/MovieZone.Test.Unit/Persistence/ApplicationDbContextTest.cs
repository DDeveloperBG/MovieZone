namespace MovieZone.Test.Unit.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using MovieZone.Domain.Entities;
    using MovieZone.Persistence;
    using NUnit.Framework;

    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertCustomerIntoDatabasee()
        {
            using var context = new ApplicationDbContext();
            var customer = new Customer();
            context.Customers.Add(customer);
            Assert.AreEqual(EntityState.Added, context.Entry(customer).State);
        }
    }
}
