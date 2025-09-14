using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Contracts.FileService.Interfaces;
using SmsDemoApp.Application.Contracts.FileService.Models;
using SmsDemoApp.Application.Contracts.User;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Domain.Entities.Ad;
using Mediator;

namespace SmsDemoApp.Application.Features.Ad.Commands;

public class CreateAdCommandHandler(
    IUnitOfWork unitOfWork
    ,IFileService fileService
    ,IUserManager userManager):IRequestHandler<CreateAdCommand,OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(CreateAdCommand request, CancellationToken cancellationToken)
    {
       
        var location = await unitOfWork.LocationRepository.GetLocationByIdAsync(request.LocationId, cancellationToken);
        
        if(location is null)
            return OperationResult<bool>.FailureResult(nameof(CreateAdCommand.LocationId),"Specified location not found");

        var category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
        
        if(category is null)
            return OperationResult<bool>.FailureResult(nameof(CreateAdCommand.CategoryId),"Specified category not found");

        var user = await userManager.GetUserByIdAsync(request.UserId, cancellationToken);
        
        if(user is null)
            return OperationResult<bool>.FailureResult(nameof(CreateAdCommand.UserId),"User not found");

        AdEntity ad;
        
        try
        {
            ad = AdEntity.Create(request.Title, request.Description, user.Id, category.Id, location.Id);
        }
        catch (Exception e)
        {
           return OperationResult<bool>.DomainFailureResult(e.Message);
        }

        if (request.AdImages.Any())
        {
            var savedImages = await fileService.SaveFilesAsync(
                request.AdImages.Select(c => new SaveFileModel(c.Base64File, c.FileContent)).ToList(), cancellationToken);
            
            savedImages.ForEach(s=>ad.AddImage(new (s.FileName,s.FileType)));
        }

        await unitOfWork.AdRepository.CreateAdAsync(ad, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        
        return OperationResult<bool>.SuccessResult(true);
    }
}