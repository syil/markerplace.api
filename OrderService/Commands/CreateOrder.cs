using FluentValidation;
using Mapster;
using MarketplaceApi.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using OrderService.Data;
using OrderService.Data.Entities;
using OrderService.Data.Enums;
using OrderService.Services;

namespace OrderService.Commands;

public record CreateOrderCommand(Guid CustomerId, List<Contracts.OrderItemInfo> OrderItems) : IRequest<OneOf<Success<Contracts.Order>, ValidationError>>;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OneOf<Success<Contracts.Order>, ValidationError>>
{
    private readonly IValidator<CreateOrderCommand> validator;
    private readonly OrderDbContext orderDbContext;
    private readonly ICustomerService customerService;
    private readonly ICatalogService catalogService;

    public CreateOrderHandler(IValidator<CreateOrderCommand> validator, OrderDbContext orderDbContext, ICustomerService customerService, ICatalogService catalogService)
    {
        this.validator = validator;
        this.orderDbContext = orderDbContext;
        this.customerService = customerService;
        this.catalogService = catalogService;
    }

    public async Task<OneOf<Success<Contracts.Order>, ValidationError>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationError(validationResult.ToDictionary());
        }

        Buyer? buyer = await orderDbContext.Buyers.FirstOrDefaultAsync(b => b.CustomerId == request.CustomerId, cancellationToken);

        if (buyer is null)
        {
            var customer = await customerService.GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                return new ValidationError("Customer not found");
            }

            buyer = new Buyer
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Lastname = customer.Lastname
            };
        }

        var order = new Order
        {
            Buyer = buyer ,
            TotalAmount = 0m,
            Status = OrderStatus.New
        };

        foreach (var item in request.OrderItems)
        {
            var product = await catalogService.GetProductAsync(item.ProductId, cancellationToken);

            if (product is null)
            {
                return new ValidationError($"Product {item.ProductId} not found");
            }

            if (product.AvailableStock < item.Quantity)
            {
                return new ValidationError($"{product.Name} out of stock");
            }

            var orderItem = new OrderItem
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Quantity = item.Quantity,
                UnitPrice = product.UnitPrice,
                TotalPrice = item.Quantity * product.UnitPrice
            };
            order.OrderItems.Add(orderItem);
            order.TotalAmount += orderItem.TotalPrice;
        }

        orderDbContext.Orders.Add(order);

        await orderDbContext.SaveChangesAsync(cancellationToken);

        var orderContract = order.Adapt<Contracts.Order>();

        return new Success<Contracts.Order>(orderContract);
    }
}