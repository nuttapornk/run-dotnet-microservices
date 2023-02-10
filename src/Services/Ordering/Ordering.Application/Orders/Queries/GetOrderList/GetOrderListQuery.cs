using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Orders.Queries.GetOrderList
{
    public record GetOrderListQuery : IRequest<List<GetOrderListResponse>>
    {
        public string Username { get; set; } = string.Empty;        
    }

    public class GetOrderListHandler : IRequestHandler<GetOrderListQuery, List<GetOrderListResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetOrderListHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<GetOrderListResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Orders
                .Where(a => a.Username == request.Username)
                .ToListAsync(cancellationToken: cancellationToken);


            return _mapper.Map<List<GetOrderListResponse>>(result);
        }
    }
}
