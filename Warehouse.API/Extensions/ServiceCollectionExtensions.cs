using Microsoft.EntityFrameworkCore;
using Warehouse.API.Mapping;
using Warehouse.API.Services;
using Warehouse.DAL;
using Warehouse.DAL.Repositories;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models;

namespace Warehouse.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<WarehouseDbContext>(options =>
            options.UseSqlite($"Data Source={configuration.GetConnectionString("DefaultConnection")}"));

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IRepository<Storage>, StorageRepository>()
            .AddScoped<IRepository<Product>, ProductRepository>()
            .AddScoped<StorageService>()
            .AddScoped<ProductService>();

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services) =>
        services.AddAutoMapper(typeof(StorageProfile))
            .AddAutoMapper(typeof(ProductProfile))
            .AddAutoMapper(typeof(StorageProductProfile))
            .AddAutoMapper(typeof(EmployeeProfile));
}