using Vigor.Common.JsonApi;

namespace Vigor.Core.Program.Api.Http.Contracts.Response;

public class Message : ContractBase
{
  public required string Content { get; set; }
}
