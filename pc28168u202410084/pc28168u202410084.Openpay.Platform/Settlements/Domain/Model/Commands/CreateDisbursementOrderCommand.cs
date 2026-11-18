using pc28168u202410084.Settlements.Domain.Model.ValueObjects;
using pc28168u202410084.Shared.Domain.Model.ValueObjects;

namespace pc28168u202410084.Settlements.Domain.Model.Commands;

/// <summary>
///     Command to create a Disbursement Order
/// </summary>
public record CreateDisbursementOrderCommand(OpenpayReference OrderTrackingCode, StoreIdentifier AffiliateStoreId, FinancialAmount TransferCap, DisbursementStatus Status);