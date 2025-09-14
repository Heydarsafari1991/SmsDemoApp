using SmsDemoApp.Domain.Entities.Ad;

namespace SmsDemoApp.Application.Features.Ad.Queries;

public record GetUserAdsQueryResult(Guid AdId,string Title,DateTime ModifiedDate,AdEntity.AdStates CurrentState);