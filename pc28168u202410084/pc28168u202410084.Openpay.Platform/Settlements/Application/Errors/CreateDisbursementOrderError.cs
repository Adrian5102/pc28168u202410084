namespace pc28168u202410084.Settlements.Application.Errors;

public enum CreateDisbursementOrderError
{
    /// <summary>
    /// A DisbursementOrder with the same data already exists.
    /// </summary>
    DuplicateDisbursementOrderId,

    /// <summary>
    /// An unexpected error occurred during the operation.
    /// </summary>
    UnexpectedError
}