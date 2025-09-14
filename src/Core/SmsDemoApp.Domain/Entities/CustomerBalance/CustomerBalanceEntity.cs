using SmsDemoApp.Domain.Common;

namespace SmsDemoApp.Domain.Entities.CustomerBalance;

public  class CustomerBalanceEntity : BaseEntity<long>
{
    public int CustomerId { get; set; }
    public decimal Balance { get; set; }
}

