namespace MiniShop.Domain.Entities;

public class PhysicalProduct(int id, string name, int basePrice, int shippingCost, int stock)
    : Product(id, name, basePrice)
{
    public int ShippingCost { get; set; } = shippingCost;
    public int Stock { get; set; } = stock;

    public override int GetFinalPrice()
    {
        return BasePrice + ShippingCost;
    }

    public override string GetDetails()
    {
        return base.GetDetails() + $"   |   Physical    | shippingCost: {ShippingCost}  |   stock: {Stock}";
    }

    public override bool CanBuy(int quantity)
    {
        return base.CanBuy(quantity) && Stock >= quantity;
    }

    public override bool Buy(int quantity)
    {
        if (!CanBuy(quantity))
            return false;

        Stock -= quantity;
        return true;
    }
}