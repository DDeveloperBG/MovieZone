namespace MovieZone.Service.Features.CustomerFeatures.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MovieZone.Domain.Entities;
    using MovieZone.Persistence;

    public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
        {
            private readonly IApplicationDbContext context;

            public GetAllCustomerQueryHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
            {
                var customerList = await this.context.Customers.ToListAsync();
                if (customerList == null)
                {
                    return null;
                }

                return customerList.AsReadOnly();
            }
        }
    }
}
