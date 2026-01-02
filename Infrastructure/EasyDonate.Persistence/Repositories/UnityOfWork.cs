using EasyDonate.Application.Interfaces;
using EasyDonate.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasyDonate.Persistence.Repositories;

public class UnityOfWork : IUnityOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction is not null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction is not null)
            await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _transaction?.Dispose();
        _context.Dispose();
        _disposed = true;
    }
}
