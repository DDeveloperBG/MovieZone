namespace MovieZone.Service.Features.CustomerFeatures.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using MovieZone.Domain.Entities;
    using MovieZone.Persistence;

    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int Id { get; set; }

        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
        {
            private readonly IApplicationDbContext context;

            public GetCustomerByIdQueryHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = this.context.Customers.Where(a => a.Id == request.Id).FirstOrDefault();
                if (customer == null)
                {
                    return null;
                }

                return Task.FromResult(customer);
            }
        }
    }
}
