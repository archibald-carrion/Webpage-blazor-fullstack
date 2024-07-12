namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

public class Acronym
{
    // TODO: add better check in TryCreate
    public string Value { get; }

    public static readonly char[] IllegalCharacters = { '/', '?', '!', '#', '@', '[', ']' };
    public const int MaxLenght = 4; // max lenght defined in the sql table

    public static readonly Acronym Invalid = new(string.Empty);

    public Acronym(string value)
    {
        // Run validation
        Value = value;
    }

    /// <summary>
    /// Check if the given string is a valid Acronym.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="acronym"></param>
    /// <returns></returns>
    public static bool TryCreate(string? value, out Acronym acronym)
    {
        // Run validation.
        acronym = Invalid;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (value.IndexOfAny(IllegalCharacters) != -1)
        {
            return false;
        }

        if (value.Length > MaxLenght)
        {
            return false;
        }
        // If validation passed, then return true and assign the Name to the out parameter.
        // Otherwise, return false
        acronym = new Acronym(value);
        return true;
    }

    /// <summary>
    /// Create a new Acronym object with the given short name string.
    /// </summary>
    /// <param name="shortNameString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Acronym Create(string shortNameString)
    {
        var result = TryCreate(shortNameString, out var acronym);
        if (!result)
        {
            throw new ArgumentException("Invalid career acronym.");
        }
        return acronym;
    }
}

