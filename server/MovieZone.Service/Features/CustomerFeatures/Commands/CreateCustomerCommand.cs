namespace MovieZone.Service.Features.CustomerFeatures.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using MovieZone.Domain.Entities;
    using MovieZone.Persistence;

    public class CreateCustomerCommand : IRequest<int>
    {
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

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
        {
            private readonly IApplicationDbContext context;

            public CreateCustomerCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = new Customer();
                customer.CustomerName = request.CustomerName;
                customer.ContactName = request.ContactName;

                this.context.Customers.Add(customer);
                await this.context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
