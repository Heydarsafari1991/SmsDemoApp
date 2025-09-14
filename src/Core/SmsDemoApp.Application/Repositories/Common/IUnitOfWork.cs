using SmsDemoApp.Application.Repositories.Ad;
using SmsDemoApp.Application.Repositories.Category;
using SmsDemoApp.Application.Repositories.CustomerBalance;
using SmsDemoApp.Application.Repositories.Location;
using SmsDemoApp.Application.Repositories.SmS;

namespace SmsDemoApp.Application.Repositories.Common;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    ILocationRepository LocationRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IAdRepository AdRepository { get; }
    ISMSRepository SMSRepository { get; }
    ICustomerBalanceRepository CustomerBalanceRepository { get; }

    Task CommitAsync(CancellationToken cancellationToken = default);     
 }