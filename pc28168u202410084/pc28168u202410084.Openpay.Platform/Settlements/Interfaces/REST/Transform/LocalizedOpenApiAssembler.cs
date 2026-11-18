using pc28168u202410084.Settlements.Interfaces.REST.Resources;
using pc28168u202410084.Resources;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;

namespace pc28168u202410084.Settlements.Interfaces.REST.Transform;

/// <summary>
///     Assembles localized OpenAPI metadata (summaries, descriptions, and response texts)
///     for the News REST endpoints and resources.
/// </summary>
public static class LocalizedOpenApiAssembler
{
    private static readonly Dictionary<string, (string Summary, string Description,
        IReadOnlyDictionary<string, string> Responses)> OperationResources =
        new Dictionary<string, (string, string, IReadOnlyDictionary<string, string>)>
        {
            ["CreateDisbursementOrder"] = (
                "CreateDisbursementOrderSummary",
                "CreateDisbursementOrderDescription",
                new Dictionary<string, string>
                {
                    ["201"] = "CreateDisbursementOrderCreated",
                    ["400"] = "CreateDisbursementOrderBadRequest",
                    ["409"] = "CreateFDisbursementOrderConflict",
                    ["500"] = "CreateDisbursementOrderServerError"
                })
        };

    /// <summary>
    ///     Localizes the summary, description, operation id and response descriptions of known operations.
    /// </summary>
    /// <param name="operation">The OpenAPI operation being transformed.</param>
    /// <param name="context">The transformer context, used to resolve the localizer and action descriptor.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A completed task.</returns>
    public static Task TransformOperationAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (context.Description.ActionDescriptor is not ControllerActionDescriptor action ||
            !OperationResources.TryGetValue(action.ActionName, out var resources))
            return Task.CompletedTask;

        var localizer = context.ApplicationServices.GetRequiredService<IStringLocalizer<SharedResource>>();
        operation.OperationId = action.ActionName;
        operation.Summary = localizer[resources.Summary].Value;
        operation.Description = localizer[resources.Description].Value;
        if (operation.Responses is null) return Task.CompletedTask;

        foreach (var (statusCode, resourceKey) in resources.Responses)
        {
            if (operation.Responses.TryGetValue(statusCode, out var response) &&
                response is OpenApiResponse openApiResponse)
                openApiResponse.Description = localizer[resourceKey].Value;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    ///     Localizes the description of known resource schemas and their properties.
    /// </summary>
    /// <param name="schema">The OpenAPI schema being transformed.</param>
    /// <param name="context">The transformer context, used to resolve the localizer and type information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A completed task.</returns>
    public static Task TransformSchemaAsync(
        OpenApiSchema schema,
        OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        var localizer = context.ApplicationServices.GetRequiredService<IStringLocalizer<SharedResource>>();
        var type = context.JsonTypeInfo.Type;
        if (type == typeof(CreateDisbursementOrderResource))
            schema.Description = localizer["CreateDisbursementOrderSchema"].Value;
        else if (type == typeof(DisbursementOrderResource))
            schema.Description = localizer["DisbursementOrderSchema"].Value;
        
        return Task.CompletedTask;
    }
}