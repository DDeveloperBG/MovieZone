﻿namespace MovieZone.Service.Features.CustomerFeatures.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using MovieZone.Persistence;

    public class UpdateCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }

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

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
        {
            private readonly IApplicationDbContext context;

            public UpdateCustomerCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var cust = this.context.Customers.Where(a => a.Id == request.Id).FirstOrDefault();

                if (cust == null)
                {
                    return default;
                }
                else
                {
                    cust.CustomerName = request.CustomerName;
                    cust.ContactName = request.ContactName;
                    this.context.Customers.Update(cust);
                    await this.context.SaveChangesAsync();
                    return cust.Id;
                }
            }
        }
    }
}
