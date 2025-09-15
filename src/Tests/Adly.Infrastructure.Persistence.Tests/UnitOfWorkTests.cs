using SmsDemoApp.Infrastructure.Persistence.Repositories.Common;
using Xunit.Abstractions;

namespace SmsDemoApp.Infrastructure.Persistence.Tests;

public class UnitOfWorkTests : IClassFixture<PersistenceTestSetup>
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IServiceProvider _serviceProvider;
    public UnitOfWorkTests(PersistenceTestSetup setup, ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _unitOfWork = setup.UnitOfWork;
        _serviceProvider = setup.ServiceProvider;
    }




}