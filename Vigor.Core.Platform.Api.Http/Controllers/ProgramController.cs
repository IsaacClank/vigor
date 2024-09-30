using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Vigor.Common.Extensions.AspNetCore;
using Vigor.Common.Extensions.System.Security.Claims;
using Vigor.Core.Platform.Common.Auth.Keycloak;
using Vigor.Core.Platform.Domain.Program.Contracts;
using Vigor.Core.Platform.Domain.Program.Interfaces;

namespace Vigor.Core.Platform.Api.Http.Controllers;

[Authorize(Policies.IsTrainingPlanOwner)]
[ApiController]
[Route("api/training-plan")]
public class TrainingPlanController(ITrainingPlanCrud trainingPlanCrud) : JsonApiController
{
  private readonly ITrainingPlanCrud _trainingPlanCrud = trainingPlanCrud;

  [HttpPost]
  public async Task<ActionResult<JsonApi.Document<TrainingPlan.Contracts.TrainingPlan>>> UpsertAsync(
    [FromBody] IEnumerable<UpsertTrainingPlan> upsertPlatforms)
  {
    return Ok(await _trainingPlanCrud.UpsertAsync(
      User.GetSubjectId(),
      upsertPlatforms));
  }

  [HttpPatch]
  public async Task<ActionResult<JsonApi.Document<TrainingPlan.Contracts.TrainingPlan>>> PatchAsync(
    [FromBody] IEnumerable<PatchTrainingPlan> patchPlatforms)
  {
    return Ok(await _trainingPlanCrud.PatchAsync(
      User.GetSubjectId(),
      patchPlatforms));
  }

  [HttpGet]
  public async Task<ActionResult<JsonApi.Document<TrainingPlan.Contracts.TrainingPlan>>> FindAsync()
  {
    return Ok(await _trainingPlanCrud.FindAsync(User.GetSubjectId()));
  }

  [HttpDelete]
  public async Task<ActionResult<JsonApi.Document<TrainingPlan.Contracts.TrainingPlan>>> RemoveAsync(
    [FromBody] IEnumerable<Guid> platformIds)
  {
    return Ok(await _trainingPlanCrud.RemoveAsync(User.GetSubjectId(), platformIds));
  }
}
