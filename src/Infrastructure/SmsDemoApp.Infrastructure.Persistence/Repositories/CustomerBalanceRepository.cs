using Microsoft.EntityFrameworkCore;
using SmsDemoApp.Application.Repositories.CustomerBalance;
using SmsDemoApp.Domain.Entities.CustomerBalance;
using SmsDemoApp.Infrastructure.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Infrastructure.Persistence.Repositories;

internal class CustomerBalanceRepository(SmsDemoAppDbContext db) : BaseRepository<CustomerBalanceEntity>(db), ICustomerBalanceRepository
{
    public async Task<bool> GetAffectedRowsOfUpdatingCustomerBalance(int customerId, decimal amount, CancellationToken cancellationToken = default)
    {
        int affectedRows = await base.TableNoTracking
    .Where(c => c.CustomerId == customerId && c.Balance >= amount)
    .ExecuteUpdateAsync(s => s
        .SetProperty(c => c.Balance, c => c.Balance - amount)
    );

        return affectedRows > 0? true : false;

    }
}
