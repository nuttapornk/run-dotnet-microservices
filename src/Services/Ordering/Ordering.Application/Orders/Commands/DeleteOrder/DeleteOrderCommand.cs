using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteOrderCommandHanderler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DeleteOrderCommandHanderler> _logger;

    public DeleteOrderCommandHanderler(IApplicationDbContext context,ILogger<DeleteOrderCommandHanderler> logger)
    {
        _context= context ?? throw new ArgumentNullException(nameof(context));
        _logger= logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = await _context.Orders.FindAsync(request.Id, cancellationToken);
        if (orderEntity == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        _context.Orders.Remove(orderEntity);
        await _context.SaveChangeAsync(cancellationToken);

        _logger.LogInformation($"Order {orderEntity.Id} is successfully deleted.");

        return Unit.Value;

    }
}