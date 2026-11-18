using pc28168u202410084.Settlements.Domain.Model.Aggregates;
using pc28168u202410084.Settlements.Domain.Model.Commands;
using pc28168u202410084.Settlements.Application.Errors;
using pc28168u202410084.Shared.Application.Patterns;

namespace pc28168u202410084.Settlements.Application.Services;
/// <summary>
///     Command service interface for favorite source operations.
/// </summary>
public interface IDisbursementOrderCommandService
{
    /// <summary>
    ///     Handle the create favorite source command.
    /// </summary>
    Task<Result<DisbursementOrder, CreateDisbursementOrderError>> Handle(CreateDisbursementOrderCommand command, CancellationToken cancellationToken = default);
}