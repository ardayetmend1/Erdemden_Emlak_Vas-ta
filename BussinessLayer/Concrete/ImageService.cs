using BussinessLayer.Abstract;

namespace BussinessLayer.Concrete;

/// <summary>
/// Görselleri dosya sistemine kaydeden servis
/// </summary>
public class ImageService : IImageService
{
    private readonly string _contentRootPath;
    private readonly string _uploadRoot;

    public ImageService(string contentRootPath)
    {
        _contentRootPath = contentRootPath;
        _uploadRoot = Path.Combine(contentRootPath, "uploads", "images");
        Directory.CreateDirectory(_uploadRoot);
    }

    public async Task<string> SaveImageAsync(string base64Data, string? fileName = null)
    {
        // data:image/jpeg;base64,xxxx formatını temizle
        var base64 = base64Data;
        var mimeType = "image/jpeg";

        if (base64.Contains(","))
        {
            var parts = base64.Split(',');
            var header = parts[0]; // data:image/jpeg;base64
            base64 = parts[1];

            if (header.Contains("image/png")) mimeType = "image/png";
            else if (header.Contains("image/webp")) mimeType = "image/webp";
            else if (header.Contains("image/gif")) mimeType = "image/gif";
        }

        var extension = mimeType switch
        {
            "image/png" => ".png",
            "image/webp" => ".webp",
            "image/gif" => ".gif",
            _ => ".jpg"
        };

        // Benzersiz dosya adı oluştur
        var uniqueName = $"{Guid.NewGuid()}{extension}";

        // Ay bazlı klasör yapısı (uploads/images/2026/03/xxx.jpg)
        var now = DateTime.UtcNow;
        var subDir = Path.Combine(now.Year.ToString(), now.Month.ToString("D2"));
        var fullDir = Path.Combine(_uploadRoot, subDir);
        Directory.CreateDirectory(fullDir);

        var filePath = Path.Combine(fullDir, uniqueName);
        var bytes = Convert.FromBase64String(base64);
        await File.WriteAllBytesAsync(filePath, bytes);

        // Relative URL döndür
        return $"/uploads/images/{subDir.Replace("\\", "/")}/{uniqueName}";
    }

    public void DeleteImage(string relativeUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl)) return;

        var fullPath = GetPhysicalPath(relativeUrl);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public string GetPhysicalPath(string relativeUrl)
    {
        // /uploads/images/2026/03/xxx.jpg -> contentRootPath/uploads/images/2026/03/xxx.jpg
        var relativePath = relativeUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
        return Path.Combine(_contentRootPath, relativePath);
    }
}
