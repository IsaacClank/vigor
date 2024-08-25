using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vigor.Core.Common.Http.Response;

public class ApiError
{
  [Required]
  public required string Title { get; set; }

  [Required]
  public required string Detail { get; set; }

  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? StackTrace { get; set; }
}

public class ApiResponse
{
  [Required]
  public Dictionary<string, object> Meta { get; set; } = [];

  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public IEnumerable<ApiError>? Errors { get; set; }

  public ApiResponse() { }
  public ApiResponse(params Exception[] exceptions)
  {
    Errors = exceptions.Select(e => new ApiError
    {
      Title = e.GetType().Name[..^2],
      Detail = e.Message,
      StackTrace = e.StackTrace,
    });
  }
}

public class ApiResponse<T>(T data) : ApiResponse
{
  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public T? Data { get; set; } = data;
}
