namespace SharedKernel.Application.DTO;

/// <summary>
/// Represents an optional value. Useful for unset properties.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Value"></param>
/// <param name="HasValue"></param>
public readonly record struct Optional<T>(T Value, bool HasValue)
{

    /// <summary>
    /// Creates an optional value with a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Optional<T> Some(T value) => new(value, true);

    /// <summary>
    /// Creates an optional value with no value (unset property).
    /// </summary>
    /// <returns></returns>
    public static Optional<T> None() => new(default!, false);

    /// <summary>
    /// Converts a value to an optional value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Optional<T>(T value) => Some(value);
}