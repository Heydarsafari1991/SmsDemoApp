using SmsDemoApp.Domain.Entities.Ad;

namespace SmsDemoApp.Application.Repositories.Category;

public interface ICategoryRepository
{
    Task CreateAsync(CategoryEntity category, CancellationToken cancellationToken = default);
    Task<List<CategoryEntity>> GetCategoriesBasedOnNameAsync(string categoryName, CancellationToken cancellationToken= default);
    Task<CategoryEntity?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
}