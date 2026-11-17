namespace MiniShop.Domain.Entities;

public abstract class Product
{
    private static int _createdCount = 0;
    public static int CreatedCount
    {
        get { return _createdCount; }
    }
    public int Id { get; }
    public string Name { get;  } 
    public int BasePrice { get; }
    public bool IsActive { get; private set; }

    public Product(int id, string name, int basePrice)
    {
        Name = name;
        Id = id;
        BasePrice = basePrice;
        _createdCount++;
    }

    //public virtual int GetFinalPrice()
    //{
    //    return BasePrice;
    //}

    public abstract int GetFinalPrice();

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
        return $"#{Id}  |   {Name}  |   ";
    }
}