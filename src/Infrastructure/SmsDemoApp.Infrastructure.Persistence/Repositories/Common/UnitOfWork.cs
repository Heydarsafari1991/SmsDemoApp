using Microsoft.EntityFrameworkCore.Storage;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Application.Repositories.CustomerBalance;
using SmsDemoApp.Application.Repositories.SmS;

namespace SmsDemoApp.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly SmsDemoAppDbContext _db;


    public UnitOfWork(SmsDemoAppDbContext db)
    {
        _db = db;

        SMSRepository = new SMSRepository(db);
        CustomerBalanceRepository = new CustomerBalanceRepository(db);
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _db.DisposeAsync();
    }



    public ISMSRepository SMSRepository { get; }
    public ICustomerBalanceRepository CustomerBalanceRepository { get; }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {

        if (_db.Database.CurrentTransaction != null)
            return _db.Database.CurrentTransaction;

        return await _db.Database.BeginTransactionAsync(cancellationToken);
    }
}