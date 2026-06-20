using Backend_Area42_3.Enums;

namespace Backend_Area42_3.Models;

public class Order
{
    public required int? Id { get; set; }

    public required int UserId { get; set; }

    public virtual OrderStatus Status { get; set; }

    public required decimal TotalPrice { get; set; }

    public required DateTime PickupTime { get; set; }

    public required List<OrderItem> Items { get; set; }
}
