using MediatR;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders;

public class DeleteOrderByDocumentNoCommand : IRequest<ApiResult<bool>>
{
    public DeleteOrderByDocumentNoCommand(string documentNo)
    {
        DocumentNo = documentNo;
    }

    public string DocumentNo { get; }
}