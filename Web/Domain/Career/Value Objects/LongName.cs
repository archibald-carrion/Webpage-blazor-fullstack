namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

// C# CLASS WRITTEN FOR THE PI CLASS
// code written by The Chayannes team


// LongName is used for multiple type such as CampusName or UniversityName and any other varchar[255]
public class LongName
{
    // TODO: add better check in TryCreate
    public string Value { get; }

    public static readonly char[] IllegalCharacters = { '/', '?', '!', '#', '@', '[', ']' };
    public const int MaxLenght = 255; // max lenght defined in the sql table

    public static readonly LongName Invalid = new(string.Empty);

    public LongName(string value)
    {
        // Run validation
        Value = value;
    }

    /// <summary>
    /// Check if the value is valid and create a LongName object.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="longName"></param>
    /// <returns></returns>

    public static bool TryCreate(string? value, out LongName longName)
    {
        // Run validation.
        longName = Invalid;
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
        longName = new LongName(value);
        return true;

    }

    /// <summary>
    /// Create a LongName object.
    /// </summary>
    /// <param name="longNameString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static LongName Create(string longNameString)
    {
        var result = TryCreate(longNameString, out var longName);
        if (!result)
        {
            throw new ArgumentException("Invalid long name.");
        }
        return longName;
    }
}
