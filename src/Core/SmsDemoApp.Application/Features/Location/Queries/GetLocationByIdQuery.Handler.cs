using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Repositories.Common;
using Mediator;

namespace SmsDemoApp.Application.Features.Location.Queries;

public class GetLocationByIdQueryHandler(IUnitOfWork unitOfWork):IRequestHandler<GetLocationByIdQuery,OperationResult<GetLocationByIdQueryResult>>
{
    public async ValueTask<OperationResult<GetLocationByIdQueryResult>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await unitOfWork.LocationRepository.GetLocationByIdAsync(request.LocationId, cancellationToken);
        
        if(location is null)
            return OperationResult<GetLocationByIdQueryResult>.NotFoundResult(nameof(GetLocationByIdQuery.LocationId),"Location not found");
        
        return OperationResult<GetLocationByIdQueryResult>.SuccessResult(new (location.Id,location.Name));
    }
}