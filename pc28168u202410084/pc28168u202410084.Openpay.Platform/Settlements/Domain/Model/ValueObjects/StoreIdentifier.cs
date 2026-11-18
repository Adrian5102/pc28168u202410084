namespace pc28168u202410084.Settlements.Domain.Model.ValueObjects;
/// <summary>
///     Value object representing a validated store identifier.
/// </summary>
public sealed record StoreIdentifier
{
    /// <summary>
    ///     Initializes a new <see cref="StoreIdentifier"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw store identifier string. Must be non-null and/or non-whitespace,</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null, empty, whitespace, or exceeds the maximum length.</exception>
    public StoreIdentifier(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("StoreIdentifier cannot be null, empty, or whitespace.", nameof(value));
        
        Value = value;
    }

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}