using System;

namespace API.Errors;

public class ApiErrorResponse(int errorCode, string message, string? details)
{
    public int ErrorCode { get; set; } = errorCode;

    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
