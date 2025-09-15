using SmsDemoApp.Application.Repositories.CustomerBalance;
using SmsDemoApp.Application.Repositories.SmS;

namespace SmsDemoApp.Application.Repositories.Common;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{

    ISMSRepository SMSRepository { get; }
    ICustomerBalanceRepository CustomerBalanceRepository { get; }

    Task CommitAsync(CancellationToken cancellationToken = default);
}