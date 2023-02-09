using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Orders.Queries.GetOrderList
{
    public record GetOrderListQuery : IRequest<List<GetOrderListResponse>>
    {
        public string Username { get; set; } = string.Empty;        
    }

    public class GetOrderListHandler : IRequestHandler<GetOrderListQuery, List<GetOrderListResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderListHandler(IOrderRepository orderRepository,IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<GetOrderListResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetOrdersByUsername(request.Username);
            return _mapper.Map<List<GetOrderListResponse>>(result);
        }
    }
}
