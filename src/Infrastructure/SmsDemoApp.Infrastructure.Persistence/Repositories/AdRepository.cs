using SmsDemoApp.Application.Repositories.Ad;
using SmsDemoApp.Domain.Entities.Ad;
using SmsDemoApp.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace SmsDemoApp.Infrastructure.Persistence.Repositories;

internal class AdRepository(SmsDemoAppDbContext db) :BaseRepository<AdEntity>(db),IAdRepository
{
    public async Task CreateAdAsync(AdEntity adEntity, CancellationToken cancellationToken = default)
    {
        await base.AddAsync(adEntity, cancellationToken);
    }

    public async Task<AdEntity?> GetAdByIdForUpdateAsync(Guid adId, CancellationToken cancellationToken = default)
    {
        return await base.Table
            .FirstOrDefaultAsync(c => c.Id.Equals(adId), cancellationToken: cancellationToken);
        
    }

    public async Task<AdEntity?> GetAdDetailByIdAsync(Guid adId, CancellationToken cancellationToken = default)
    {
        return await base.TableNoTracking
            .Include(c => c.User)
            .Include(c => c.Category)
            .Include(c => c.Location)
            .FirstOrDefaultAsync(c => c.Id.Equals(adId), cancellationToken: cancellationToken);
    }

    public async Task<List<AdEntity>> GetUserAdsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await base.TableNoTracking
            .Where(c => c.UserId.Equals(userId))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AdEntity>> GetVerifiedAdsAsync(int currentPage, int pageCount, CancellationToken cancellationToken = default)
    {
        if (pageCount <= 0)
            pageCount = 10;
        if (currentPage <= 0)
            currentPage = 1;

        return await base.TableNoTracking
            .Where(c => c.CurrentState == AdEntity.AdStates.Approved)
            .Skip((currentPage - 1) * pageCount)
            .Take(pageCount)
            .ToListAsync(cancellationToken);
    }
}