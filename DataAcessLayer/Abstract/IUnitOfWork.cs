using EntityLayer.Entities;

namespace DataAcessLayer.Abstract;

/// <summary>
/// Unit of Work interface - Transaction yönetimi ve repository erişimi
/// </summary>
public interface IUnitOfWork : IDisposable
{
    // Generic repository erişimi
    IGenericRepository<T> Repository<T>() where T : BaseEntity;

    // Transaction işlemleri
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
