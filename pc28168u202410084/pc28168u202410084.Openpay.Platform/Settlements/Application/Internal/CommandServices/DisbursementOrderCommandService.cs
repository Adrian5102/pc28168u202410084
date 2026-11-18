using pc28168u202410084.Settlements.Domain.Model.Aggregates;
using pc28168u202410084.Settlements.Application.Errors;
using pc28168u202410084.Settlements.Application.Services;
using pc28168u202410084.Settlements.Domain.Model.Commands;
using pc28168u202410084.Shared.Domain.Repositories;
using pc28168u202410084.Shared.Application.Patterns;
using Microsoft.EntityFrameworkCore;

namespace pc28168u202410084.Settlements.Application.Internal.CommandServices;

/// <summary>
///     Application service for handling favorite source creation commands.
/// </summary>

public class FavoriteSourceCommandService(
    IFavoriteSourceRepository favoriteSourceRepository,
    IUnitOfWork unitOfWork,
    ILogger<FavoriteSourceCommandService> logger)
    : IFavoriteSourceCommandService
{
    /// <inheritdoc />
    public async Task<Result<DisbursementOrder, CreateDisbursementOrderError>> Handle(CreateDisbursementOrderCommand command,
        CancellationToken cancellationToken = default) {
        
    }

    /// <summary>
    /// Determines whether a DbUpdateException represents a duplicate key constraint violation.
    /// </summary>
    /// <param name="exception">The exception to inspect.</param>
    /// <returns>True if the exception is due to a MySQL duplicate key error (code 1062), false otherwise.</returns>
    private static bool IsDuplicateKeyViolation(DbUpdateException exception)
    {
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            if (!string.Equals(current.GetType().Name, "MySqlException", StringComparison.Ordinal)) continue;
            var numberProperty = current.GetType().GetProperty("Number");
            if (numberProperty?.PropertyType == typeof(int) &&
                numberProperty.GetValue(current) is int errorCode &&
                errorCode == 1062)
                return true;
        }
        return false;
    }
}