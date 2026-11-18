namespace pc28168u202410084.Shared.Domain.Model.ValueObjects;

public sealed record FinancialAmount
{
    public decimal Amount;
    public string CurrencyCode;
    
    public FinancialAmount(string value)
    {
        if (Amount < decimal.Zero) 
            throw new ArgumentOutOfRangeException("Amount must be greater than zero.", nameof(value));
        
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CurrencyCode cannot be null, empty, or whitespace.", nameof(value));
        
        Value = value;
    }
    
    public string Value { get; }

    public override string ToString() => Value;
}