using MediatR;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Unit>
{
    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Dependencies are usually injected via constructor,
        // but sometimes method injection can be useful
    }
}