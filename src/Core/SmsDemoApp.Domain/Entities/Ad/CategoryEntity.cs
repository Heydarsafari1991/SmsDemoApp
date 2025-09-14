using SmsDemoApp.Domain.Common;

namespace SmsDemoApp.Domain.Entities.Ad;

public sealed class CategoryEntity:BaseEntity<Guid>
{
    public string Name { get;private set; }

    private List<AdEntity> _ads = new();

    public IReadOnlyList<AdEntity> Ads => _ads.AsReadOnly();

    public CategoryEntity(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    private CategoryEntity()
    {
        
    }
}