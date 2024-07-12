namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.ValueObjects;

public class AcronymFixture
{
    public string ValidAcronym { get; } = "CS"; // Correct acronym
    public string InvalidAcronymTooLong { get; } = "COMPTSCI"; // Acronym too long
    public string InvalidAcronymIllegalChar { get; } = "C@S!"; // Acronym with illegal characters
    public string InvalidAcronymEmpty { get; } = ""; // Empty acronym
}