namespace Core.DTOs.Common;

/// <summary>
/// Standart API yanıt wrapper'ı
/// </summary>
public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponseDto<T> SuccessResponse(T data, string? message = null, string? errorCode = null)
    {
        return new ApiResponseDto<T>
        {
            Success = true,
            Data = data,
            Message = message,
            ErrorCode = errorCode
        };
    }

    public static ApiResponseDto<T> FailResponse(string message, string? errorCode = null, List<string>? errors = null)
    {
        return new ApiResponseDto<T>
        {
            Success = false,
            Message = message,
            ErrorCode = errorCode,
            Errors = errors
        };
    }
}

/// <summary>
/// Data içermeyen API yanıtları için
/// </summary>
public class ApiResponseDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponseDto SuccessResponse(string? message = null)
    {
        return new ApiResponseDto
        {
            Success = true,
            Message = message
        };
    }

    public static ApiResponseDto FailResponse(string message, List<string>? errors = null)
    {
        return new ApiResponseDto
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
}
