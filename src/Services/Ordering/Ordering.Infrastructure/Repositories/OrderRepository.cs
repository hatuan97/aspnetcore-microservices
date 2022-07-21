using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Features.V1.Orders;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Extensions;
using Ordering.Infrastructure.Persistence;
using Shared.SeedWork;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order, long, OrderContext>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext, IUnitOfWork<OrderContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public IQueryable<Order> GetOrderPaginationQueryable(GetOrderParameters parameters)
    {
        return FindAll()
            .Sort(parameters.OrderBy)
            .Search(parameters.SearchTerm);
    }

    public async Task<PagedList<Order>> GetOrderPagination(GetOrderParameters parameters)
    {
        return await GetOrderPaginationQueryable(parameters)
            .PaginatedListAsync(parameters.PageNumber, parameters.PageSize);
        
        // return await FindAll()
        //     .Sort(parameters.OrderBy)
        //     .Search(parameters.SearchTerm)
        //     .PaginatedListAsync(parameters.PageNumber, parameters.PageSize);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName) =>
        await FindByCondition(x => x.UserName.Equals(userName))
            .ToListAsync();

    public void CreateOrder(Order order) => Create(order);

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        await UpdateAsync(order);
        return order;
    }

    public void DeleteOrder(Order order) => Delete(order);
}