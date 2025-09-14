using SmsDemoApp.Application.Extensions;
using SmsDemoApp.Application.Features.Category.Queries;
using SmsDemoApp.Application.Repositories.Category;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Application.Tests.Extensions;
using SmsDemoApp.Domain.Entities.Ad;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit.Abstractions;

namespace SmsDemoApp.Application.Tests;

public class CategoryFeaturesTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITestOutputHelper _testOutputHelper;

    public CategoryFeaturesTests(ITestOutputHelper testOutputHelper)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.RegisterApplicationValidators();

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Get_Category_By_Id_Should_Return_Success()
    {
        //Arrange

        var getCategoryByIdQuery = new GetCategoryByIdQuery(Guid.NewGuid());

        var unitOfWork = Substitute.For<IUnitOfWork>();
        var categoryRepository = Substitute.For<ICategoryRepository>();

        categoryRepository.GetCategoryByIdAsync(getCategoryByIdQuery.CategoryId)!
            .Returns(Task.FromResult(new CategoryEntity("Test")));

        unitOfWork.CategoryRepository.Returns(categoryRepository);

        var handler = new GetCategoryByIdQueryHandler(unitOfWork);
        
        //Act

        var result = await Helpers.ValidateAndExecuteAsync(getCategoryByIdQuery, handler, _serviceProvider);
        
        //Assert

        result.Result.Should().NotBeNull();
    }
    
    
    [Fact]
    public async Task Get_Category_By_Empty_Id_Should_Return_Not_Success()
    {
        //Arrange

        var getCategoryByIdQuery = new GetCategoryByIdQuery(Guid.Empty);

        var unitOfWork = Substitute.For<IUnitOfWork>();
        var categoryRepository = Substitute.For<ICategoryRepository>();

        categoryRepository.GetCategoryByIdAsync(getCategoryByIdQuery.CategoryId)!
            .Returns(Task.FromResult(new CategoryEntity("Test")));

        unitOfWork.CategoryRepository.Returns(categoryRepository);

        var handler = new GetCategoryByIdQueryHandler(unitOfWork);
        
        //Act

        var result = await Helpers.ValidateAndExecuteAsync(getCategoryByIdQuery, handler, _serviceProvider);
        
        //Assert

        result.Result.Should().BeNull();
        
        _testOutputHelper.WritelineOperationResultErrors(result);
    }
}