using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Domain.Entities.Ad;
using Mediator;

namespace SmsDemoApp.Application.Features.Category.Commands;

public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork):IRequestHandler<CreateCategoryCommand,OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CategoryEntity(request.CategoryName);

        await unitOfWork.CategoryRepository.CreateAsync(category, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
        
        return OperationResult<bool>.SuccessResult(true);
    }
}