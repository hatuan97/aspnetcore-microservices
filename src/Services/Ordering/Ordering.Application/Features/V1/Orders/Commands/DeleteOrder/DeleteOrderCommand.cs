using MediatR;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders;

public class DeleteOrderCommand : IRequest<ApiResult<bool>>
{
    public DeleteOrderCommand(long id)
    {
        Id = id;
    }

    public long Id { get; }
}