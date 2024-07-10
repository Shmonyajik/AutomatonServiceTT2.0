using System.Collections.Concurrent;
using Warehouse.Domain.Tools.Exceptions;

namespace Warehouse.Domain.Models;

public class Storage : BaseEntity
{
    private ConcurrentDictionary<long, Employee>? _employeesDictionary;
    private ConcurrentDictionary<long, StorageProduct>? _productsDictionary;
    private List<StorageProduct> _products = new();
    private List<Employee> _employees = new();

    public IReadOnlyList<StorageProduct> Products => _products;
    public IReadOnlyList<Employee> Employees => _employees;

    public IReadOnlyDictionary<long, Employee>? EmployeesDictionary
    {
        get
        {
            _employeesDictionary ??=
                new ConcurrentDictionary<long, Employee>(Employees.ToDictionary(e => e.Id, e => e));

            return _employeesDictionary;
        }
    }

    public IReadOnlyDictionary<long, StorageProduct>? ProductsDictionary
    {
        get
        {
            _productsDictionary ??=
                new ConcurrentDictionary<long, StorageProduct>(Products.ToDictionary(sp => sp.ProductId, sp => sp));

            return _productsDictionary;
        }
    }

    public void AddEmployee(Employee employee)
    {
        if (employee == null)
            throw new DomainException("Employee can't be null", new ArgumentNullException(nameof(employee)));

        if (EmployeesDictionary.ContainsKey(employee.Id))
            throw new DomainException($"Employee {employee.Id} already attached to the Storage {Id}");

        _employeesDictionary.TryAdd(employee.Id, employee);
    }

    public void AddProduct(long productId)
    {
        if (ProductsDictionary is not null && !_productsDictionary.TryAdd(productId, new StorageProduct(Id, productId)))
            throw new DomainException($"Storage already contains the product with id={productId}");
    }

    public void IncreaseProductQuantity(long productId, int quantity)
    {
        var storageProduct = ProductsDictionary[productId];
        if (storageProduct == null)
            throw new DomainException($"Storage doesn't contain the product with id={productId}");

        storageProduct.AddQuantity(quantity);
    }

    public void SyncCollections()
    {
        _products = ProductsDictionary.Values.ToList();
        _employees = EmployeesDictionary.Values.ToList();
    }
}