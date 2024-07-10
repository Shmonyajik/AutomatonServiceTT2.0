namespace Warehouse.Domain.Models;

public class Product : BaseEntity
{
    private string _name;

    protected Product()
    {
    }

    public Product(string name)
    {
        Name = name;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
            _name = value;
        }
    }
}