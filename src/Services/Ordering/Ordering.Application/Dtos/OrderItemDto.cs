

namespace Ordering.Application.Dtos;

public record OrderItemDto(
    Guid Orderİd, Guid ProductId, int Quantity, decimal Price
    );