namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.ValueObjects;

public class MediumNameFixture
{
    public string ValidMediumName { get; } = "Computer Science"; // Valid medium name
    public string InvalidMediumNameTooLong { get; } = new string('A', 128); // 128 is the max length of the medium name
    public string InvalidMediumNameIllegalChar { get; } = "Computer@Science"; // @ is an illegal character
    public string InvalidMediumNameEmpty { get; } = "";     // Empty string
}
