using Warehouse.Domain.Tools.Exceptions;

namespace Warehouse.Domain.Models;

public class Employee : BaseEntity
{
    private string _name;
    private readonly List<Storage> _storages;

    protected Employee()
    {
    }

    public Employee(List<Storage> storages, string name)
    {
        _storages = storages ?? throw new DomainException("List of storages can't be null",
            new ArgumentNullException(nameof(storages)));

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

    public IReadOnlyList<Storage> Storages => _storages;
}