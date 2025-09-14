using SmsDemoApp.Application.Repositories.Category;
using SmsDemoApp.Domain.Entities.Ad;
using SmsDemoApp.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace SmsDemoApp.Infrastructure.Persistence.Repositories;

internal class CategoryRepository(SmsDemoAppDbContext db) :BaseRepository<CategoryEntity>(db),ICategoryRepository
{
    public async Task CreateAsync(CategoryEntity category, CancellationToken cancellationToken = default)
    {
        await base.AddAsync(category, cancellationToken);
    }

    public async Task<List<CategoryEntity>> GetCategoriesBasedOnNameAsync(string categoryName, CancellationToken cancellationToken = default)
    {
        return await base.TableNoTracking
            .Where(c => c.Name.Contains(categoryName))
            .ToListAsync(cancellationToken);
    }

    public async Task<CategoryEntity?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await base.TableNoTracking
            .FirstOrDefaultAsync(c => c.Id.Equals(categoryId), cancellationToken: cancellationToken);
    }
}