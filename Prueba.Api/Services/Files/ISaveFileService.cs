namespace Prueba.Api.Services.Files
{
    public interface ISaveFileService
    {
        string SaveFileAsync(IFormFile file);
    }
}
