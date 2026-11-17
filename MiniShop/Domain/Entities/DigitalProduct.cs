namespace MiniShop.Domain.Entities;

public class DigitalProduct(int id, string name, int basePrice, int discountPercent)
    : Product(id, name, basePrice)
{
    public int DiscountPercent { get; } = discountPercent;
    public override int GetFinalPrice()
    {
        return BasePrice -20000;
    }

    public override string GetDetails()
    {
        return base.GetDetails() + $" | Digital | Discount: {DiscountPercent}%";
    }
}