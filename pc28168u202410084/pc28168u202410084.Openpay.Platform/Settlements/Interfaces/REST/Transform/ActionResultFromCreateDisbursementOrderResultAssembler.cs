using pc28168u202410084.Settlements.Application.Errors;
using pc28168u202410084.Settlements.Domain.Model.Aggregates;
using pc28168u202410084.Resources;
using pc28168u202410084.Shared.Application.Patterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace pc28168u202410084.Settlements.Interfaces.REST.Transform;


public static class ActionResultFromCreateDisbursementOrderResultAssembler
{
    public static ActionResult ToActionResultFromCreateFavoriteSourceResult(
        Result<DisbursementOrder, CreateDisbursementOrderError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer,
        string getFavoriteSourceByIdActionName) =>
        result switch
        {
            Result<DisbursementOrder, CreateFDisbursementOrderError>.Success success =>
                controller.CreatedAtAction(getFavoriteSourceByIdActionName, new { id = success.Value.Id },
                    DisbursementOrderResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<DisbursementOrder, CreateDisbursementOrderError>.Failure failure =>
                failure.Error switch
                {
                    CreateDisbursementOrderError.UnexpectedError =>
                        controller.Problem(
                            title: localizer["UnexpectedServerError"].Value,
                            detail: localizer["UnexpectedErrorCreatingDisbursementOrder"].Value,
                            statusCode: 500),

                    _ => controller.Problem(
                        title: localizer["UnexpectedServerError"].Value,
                        detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                        statusCode: 500)
                },

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorProcessingRequest"].Value,
                statusCode: 500)
        };
}