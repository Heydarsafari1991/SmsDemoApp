using Microsoft.EntityFrameworkCore;
using SmsDemoApp.Application.Repositories.SmS;
using SmsDemoApp.Domain.Entities.Ad;
using SmsDemoApp.Domain.Entities.Message;
using SmsDemoApp.Infrastructure.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Infrastructure.Persistence.Repositories;

internal class SMSRepository(SmsDemoAppDbContext db) : BaseRepository<SMSEntity>(db), ISMSRepository
{
    public async Task CreateAsync(SMSEntity SMSEntity, CancellationToken cancellationToken = default)
    {
        await base.AddAsync(SMSEntity, cancellationToken);
    }


    public async Task<SMSEntity> GetByIdAysnc(long id, CancellationToken cancellationToken = default)
    {
        return await base.TableNoTracking
            .FirstOrDefaultAsync(c => c.Id==id, cancellationToken: cancellationToken);
    }

    public async Task<(IEnumerable<SMSEntity>, int totalCount)> Search(int customerId, MessageDelivaryStatus? status = null, string? text = null, string? reciverPhoneNumber = null, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var query =  base.TableNoTracking
            .Where(c => c.CustomerId.Equals(customerId)
            && (status == null || c.Status == status)
            && (text == null || c.Text == text)
            && (reciverPhoneNumber == null || c.ReciverPhoneNumber == reciverPhoneNumber)

            );
        var totalCount = await query.CountAsync(cancellationToken);
        var data = await query
           .Skip((page - 1) * pageSize).Take(pageSize)
           .ToListAsync(cancellationToken);

        return (data, totalCount);

    }
}
