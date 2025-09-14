using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Repositories.Common;
using Mediator;

namespace SmsDemoApp.Application.Features.Category.Queries;

public class GetCategoriesByNameQueryHandler(IUnitOfWork unitOfWork):IRequestHandler<GetCategoriesByNameQuery,OperationResult<List<GetCategoriesByNameQueryResult>>>
{
    public async ValueTask<OperationResult<List<GetCategoriesByNameQueryResult>>> Handle(GetCategoriesByNameQuery request, CancellationToken cancellationToken)
    {
        var categories =
            await unitOfWork.CategoryRepository.GetCategoriesBasedOnNameAsync(request.CategoryName, cancellationToken);
        
        return OperationResult<List<GetCategoriesByNameQueryResult>>
            .SuccessResult(categories.Select(c=>new GetCategoriesByNameQueryResult(c.Id,c.Name)).ToList());
    }
}