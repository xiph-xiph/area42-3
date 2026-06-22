using Backend_Area42_3.Enums;

namespace Backend_Area42_3.Models;

public class Cart : Order
{
    public override OrderStatus Status
    {
        get => OrderStatus.Cart;
        set { }
    }
}
