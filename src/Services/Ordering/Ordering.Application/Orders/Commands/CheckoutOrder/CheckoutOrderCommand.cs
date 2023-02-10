using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.CheckoutOrder;

public record CheckoutOrderCommand : IRequest<int>
{
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

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    public CheckoutOrderCommandHandler(IApplicationDbContext context,ILogger<CheckoutOrderCommandHandler> logger,IMapper mapper)
    {
        _context= context ?? throw new ArgumentNullException(nameof(context));
        _logger= logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper= mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        if (orderEntity == null)
        {
            throw new ArgumentNullException(nameof(orderEntity));
        }

        _context.Orders.Add(orderEntity);
        await _context.SaveChangeAsync(cancellationToken);

        _logger.LogInformation($"Order {orderEntity.Id} is successfully create.");
        return orderEntity.Id;
    }
}