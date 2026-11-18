using pc28168u202410084.Settlements.Domain.Model.Commands;
using pc28168u202410084.Settlements.Domain.Model.ValueObjects;
using pc28168u202410084.Shared.Domain.Model.ValueObjects;

namespace pc28168u202410084.Settlements.Domain.Model.Aggregates;

public partial class DisbursementOrder
{
    
    public DisbursementOrder(CreateDisbursementOrderCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
    }

    /// <summary>
    ///     Gets the server-generated identifier for this Disbursement Order.
    /// </summary>
    public int Id { get; private set; }
    
    /// <summary>
    ///     Gets the OrderTrackingCode.
    /// </summary>
    public OpenpayReference OrderTrackingCode { get; private set; }
    /// <summary>
    ///     Gets the AffiliateStoreId.
    /// </summary>
    public StoreIdentifier AffiliateStoreId { get; private set; }
    /// <summary>
    ///     Gets the TransferCap.
    /// </summary>
    public FinancialAmount TransferCap { get; private set; }
    /// <summary>
    ///     Gets the Status.
    /// </summary>
    public DisbursementStatus Status { get; private set; }
}