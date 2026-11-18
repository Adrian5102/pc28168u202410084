using System.Net.Mime;
using pc28168u202410084.Settlements.Application.Services;
using pc28168u202410084.Settlements.Domain.Model.ValueObjects;
using pc28168u202410084.Settlements.Interfaces.REST.Resources;
using pc28168u202410084.Settlements.Interfaces.REST.Transform;
using pc28168u202410084.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace pc28168u202410084.Settlements.Interfaces.REST;

/// <summary>
///     Favorite sources' controller.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Disbursement Order")]
public class FavoriteSourcesController(
    IDisbursementOrderCommandService disbursementOrderCommandService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<FavoriteSourcesController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Creates a Disbursement Order.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DisbursementOrderResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateFavoriteSource([FromBody] CreateDisbursementOrderResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var createDisbursementOrderCommand =
                CreateDisbursementOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await disbursementOrderCommandService.Handle(createDisbursementOrderCommand, cancellationToken);
            return ActionResultFromCreateDisbursementOrderResultAssembler.ToActionResultFromCreateFDisbursementOrderResult(
                result,
                this,
                localizer);
        }
        catch (Exception ex)
        { 
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingFavoriteSource"].Value,
                statusCode: 500);
        }
    }
}