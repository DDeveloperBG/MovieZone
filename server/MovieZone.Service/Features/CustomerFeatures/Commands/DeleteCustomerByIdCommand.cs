namespace MovieZone.Service.Features.CustomerFeatures.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MovieZone.Persistence;

    public class DeleteCustomerByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommand, int>
        {
            private readonly IApplicationDbContext context;

            public DeleteCustomerByIdCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
            {
                var customer = await this.context.Customers.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (customer == null)
                {
                    return default;
                }

                this.context.Customers.Remove(customer);
                await this.context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
