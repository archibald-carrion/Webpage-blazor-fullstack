namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.ValueObjects;

public class LongNameFixture
{
    public string ValidLongName { get; } = "Computer Science School"; // valid long name
    public string InvalidLongNameTooLong { get; } = new string('A', 256); // invalid long name, too long
    public string InvalidLongNameIllegalChar { get; } = "Department@CS"; // invalid long name, has illegal character @
    public string InvalidLongNameEmpty { get; } = ""; // invalid long name, empty
}
