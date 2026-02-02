using System.Collections;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAcessLayer.Concrete;

/// <summary>
/// Unit of Work implementation - Transaction ve repository yönetimi
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;
    private readonly Hashtable _repositories;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnitOfWork(Context context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    /// <summary>
    /// Generic repository erişimi - Her entity tipi için tek bir repository instance
    /// </summary>
    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        var typeName = typeof(T).Name;

        if (!_repositories.ContainsKey(typeName))
        {
            var repositoryInstance = new GenericRepository<T>(_context);
            _repositories.Add(typeName, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[typeName]!;
    }

    /// <summary>
    /// Değişiklikleri veritabanına kaydeder
    /// </summary>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Yeni transaction başlatır
    /// </summary>
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Transaction'ı commit eder
    /// </summary>
    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();

            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    /// <summary>
    /// Transaction'ı geri alır
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <summary>
    /// Kaynakları serbest bırakır
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
