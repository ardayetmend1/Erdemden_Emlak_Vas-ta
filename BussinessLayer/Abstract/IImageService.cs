namespace BussinessLayer.Abstract;

/// <summary>
/// Görsel dosya yönetimi servisi
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Base64 veriyi dosya olarak kaydeder, relative URL döner (örn: /uploads/images/xxx.jpg)
    /// </summary>
    Task<string> SaveImageAsync(string base64Data, string? fileName = null);

    /// <summary>
    /// Dosyayı siler (relative URL alır)
    /// </summary>
    void DeleteImage(string relativeUrl);

    /// <summary>
    /// Dosyanın fiziksel yolunu döner
    /// </summary>
    string GetPhysicalPath(string relativeUrl);
}
