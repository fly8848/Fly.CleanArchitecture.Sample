using Fly.CleanArchitecture.Sample.Application.Orders.Commands;
using MediatR;

namespace Fly.CleanArchitecture.Sample.Api.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
[Response]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [UnitOfWork]
    // [Validator]
    [HttpPost]
    public async Task CreateOrderAsync(CreateOrderCommand command)
    {
        await _mediator.Send(command);
    }
}