using SmsDemoApp.Application.Common;
using Xunit.Abstractions;

namespace SmsDemoApp.Application.Tests.Extensions;

public static class ApplicationTestExtensions
{
    public static void WritelineOperationResultErrors<TResult>(this ITestOutputHelper testOutputHelper,
        OperationResult<TResult> operationResult)
    {
        foreach (var error in operationResult.ErrorMessages)
        {
            testOutputHelper.WriteLine($"Property Name:{error.Key} Message:{error.Value}");
        }
    }
}