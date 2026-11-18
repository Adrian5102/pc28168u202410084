namespace pc28168u202410084.Settlements.Domain.Model.ValueObjects;
/// <summary>
///     Value object representing a validated openpay reference.
/// </summary>
public sealed record OpenpayReference
{
    /// <summary>
    ///     Initializes a new <see cref="OpenpayReference"/> with the given raw string value.
    /// </summary>
    /// <param name="value">The raw openpay reference string. Must be non-null and/or non-whitespace.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null, empty or whitespace.</exception>
    public OpenpayReference(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("OpenpayReference cannot be null, empty, or whitespace.", nameof(value));

        Value = value;
    }

    /// <summary>
    ///     Gets the underlying primitive value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString() => Value;
}