
using SmsDemoApp.Domain.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Application.Repositories.SmS;

public interface ISMSRepository
{
    Task CreateAsync(SMSEntity SMSEntity, CancellationToken cancellationToken = default);
    Task<(IEnumerable<SMSEntity>, int totalCount)> Search(int customerId, MessageDelivaryStatus? status = null, string? text = null
        , string? ReciverPhoneNumber = null,int page = 0 ,int pageSize = 10 ,CancellationToken cancellationToken = default);
    Task<SMSEntity> GetByIdAysnc(Guid id, CancellationToken cancellationToken = default);
}
