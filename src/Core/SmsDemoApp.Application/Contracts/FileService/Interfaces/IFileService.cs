using SmsDemoApp.Application.Contracts.FileService.Models;

namespace SmsDemoApp.Application.Contracts.FileService.Interfaces;

public interface IFileService
{
    Task<List<SaveFileModelResult>> SaveFilesAsync(List<SaveFileModel> files,
        CancellationToken cancellationToken = default);

    Task<List<GetFileModel>> GetFilesByNameAsync(List<string> fileNames, CancellationToken cancellationToken = default);

    Task RemoveFilesAsync(string[] fileNames, CancellationToken cancellationToken = default);
}