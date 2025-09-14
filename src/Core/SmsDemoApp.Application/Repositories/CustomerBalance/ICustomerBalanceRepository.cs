using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Application.Repositories.CustomerBalance;

public interface ICustomerBalanceRepository
{
    Task<bool> GetAffectedRowsOfUpdatingCustomerBalance(int customerId, decimal amount, CancellationToken cancellationToken = default);
}
