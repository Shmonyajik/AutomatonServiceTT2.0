using AutoMapper;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models;

namespace Warehouse.API.Services;

public abstract class BaseService<T> where T : BaseEntity
{
    protected BaseService(IRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Mapper = mapper;
    }

    protected IRepository<T> Repository { get; }
    protected IUnitOfWork UnitOfWork { get; }
    protected IMapper Mapper { get; }
}