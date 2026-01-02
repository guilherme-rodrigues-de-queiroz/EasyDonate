namespace EasyDonate.Application.Interfaces;

public interface IUnityOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}