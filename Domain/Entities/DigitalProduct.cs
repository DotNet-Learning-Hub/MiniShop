namespace MiniShop.Domain.Entities;

public class DigitalProduct : Product
{
    public int DiscountPercent { get; }

    public DigitalProduct(int id, string name, int basePrice, int discountPercent)
        : base(id, name, basePrice)
    {
        DiscountPercent = discountPercent;
    }

    public override int GetFinalPrice()
    {
        return BasePrice * (100 - DiscountPercent) / 100;
    }

    public override string GetDetails()
    {
        return base.GetDetails() + $" | Digital | Discount: {DiscountPercent}%";
    }
}
