using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.UpdateOrder.v1;

public record UpdateOrderCommand : IRequest
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    // BillingAddress
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    // Payment
    public string CardName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string Expiration { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public int PaymentMethod { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    public UpdateOrderCommandHandler(IApplicationDbContext context, ILogger<UpdateOrderCommandHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync(request.Id, cancellationToken);
        if (order == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        _mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(Order));

        _context.Orders.Update(order);
        await _context.SaveChangeAsync(cancellationToken);

        _logger.LogInformation($"Order {order.Id} is successfully updated.");
        return Unit.Value;
    }
}