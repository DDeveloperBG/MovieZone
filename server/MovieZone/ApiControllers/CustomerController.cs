namespace MovieZone.ApiControllers
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using MovieZone.Service.Features.CustomerFeatures.Commands;
    using MovieZone.Service.Features.CustomerFeatures.Queries;

    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/Customer")]
    [ApiVersion("1.0")]
    public class CustomerController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            return this.Ok(await this.Mediator.Send(command));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return this.Ok(await this.Mediator.Send(new GetAllCustomerQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return this.Ok(await this.Mediator.Send(new GetCustomerByIdQuery { Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return this.Ok(await this.Mediator.Send(new DeleteCustomerByIdCommand { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.Mediator.Send(command));
        }
    }
}
