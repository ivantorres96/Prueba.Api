
namespace Prueba.Api.Services.Files
{
    public class SaveFileService(IWebHostEnvironment webHostEnvironment) : ISaveFileService
    {
        public string SaveFileAsync(IFormFile file)
        {
			try
			{
				var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var path = Path.Combine(webHostEnvironment.WebRootPath, "Files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return fileName;    
            }
			catch (Exception)
			{
				return string.Empty;
			}
        }
    }
}
