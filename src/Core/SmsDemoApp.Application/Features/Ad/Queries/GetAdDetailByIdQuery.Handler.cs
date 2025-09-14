using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Contracts.FileService.Interfaces;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Domain.Entities.Ad;
using AutoMapper;
using Mediator;

namespace SmsDemoApp.Application.Features.Ad.Queries;

public class GetAdDetailByIdQueryHandler(IUnitOfWork unitOfWork,IFileService fileService,IMapper mapper)
:IRequestHandler<GetAdDetailByIdQuery,OperationResult<GetAdDetailByIdQueryResult>>
{
    public async ValueTask<OperationResult<GetAdDetailByIdQueryResult>> Handle(GetAdDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var ad = await unitOfWork.AdRepository.GetAdDetailByIdAsync(request.AdId, cancellationToken);
        
        if(ad is null)
            return OperationResult<GetAdDetailByIdQueryResult>.FailureResult(nameof(GetAdDetailByIdQuery.AdId),"Specified Ad not found");

        var adImages =
            await fileService.GetFilesByNameAsync(ad.Images.Select(c => c.FileName).ToList(), cancellationToken);

        var result = mapper.Map<AdEntity, GetAdDetailByIdQueryResult>(ad);

        result.AdImages = adImages.Select(c => new GetAdDetailByIdQueryResult.AdDetailImageModel(c.FileName, c.FileUrl))
            .ToArray();
        
        
        return OperationResult<GetAdDetailByIdQueryResult>.SuccessResult(result);

    }
}