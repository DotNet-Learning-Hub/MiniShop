namespace MiniShop.Domain.Entities;

public class PhysicalProduct : Product
{
    public int ShippingCost { get; }
    public int Stock { get; private set; }

    public PhysicalProduct(int id, string name, int basePrice, int shippingCost, int stock)
        : base(id, name, basePrice)
    {
        ShippingCost = shippingCost;
        Stock = stock;
    }

    public override int GetFinalPrice()
    {
        return BasePrice + ShippingCost;
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

    public override string GetDetails()
    {
        return base.GetDetails() + $" | Physical | Stock: {Stock}";
    }
}
