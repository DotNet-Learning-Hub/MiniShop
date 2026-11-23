namespace MiniShop.Domain.Entities;

// از خود Product نمی توان مستقیم نمونه ساخت.
public abstract class Product
{
    private static int _createdCount = 0;

    public static int CreatedCount { get { return _createdCount; } }

    public int Id { get; }
    public string Name { get; }
    public int BasePrice { get; }
    public bool IsActive { get; private set; }

    // سازنده کلاس پایه هنگام ساخت محصول فیزیکی یا دیجیتال اجرا می شود.
    protected Product(int id, string name, int basePrice)
    {
        Id = id;
        Name = name;
        BasePrice = basePrice;
        IsActive = true;
        _createdCount++;
    }

    // هر فرزند باید قیمت نهایی را به روش خودش محاسبه کند.
    public abstract int GetFinalPrice();

    // رفتار پیش فرض؛ فرزند در صورت نیاز آن را تغییر می دهد.
    public virtual bool CanBuy(int quantity)
    {
        return IsActive && quantity > 0;
    }

    public virtual bool Buy(int quantity)
    {
        return CanBuy(quantity);
    }

    public virtual string GetDetails()
    {
        return $"#{Id} | {Name} | Price: {GetFinalPrice():N0}";
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
