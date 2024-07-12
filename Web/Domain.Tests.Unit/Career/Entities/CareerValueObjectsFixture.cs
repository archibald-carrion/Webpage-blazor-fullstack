using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.Entities;

public class CareerValueObjectsFixture
{
    private const string kAcronymValue = "CS"; // Correct acronym
    private const string kNameValue = "Computer Science"; // Correct name
    private const string kDescriptionValue = "A study of computation, information processing, and systems."; // Correct description

    public Acronym Acronym { get; set; }
    public LongName Name { get; set; }
    public LongName Description { get; set; }

    /// <summary>
    /// constructor that initializes the fixture.
    /// </summary>
    public CareerValueObjectsFixture()
    {
        Acronym = Acronym.Create(kAcronymValue);
        Name = LongName.Create(kNameValue);
        Description = LongName.Create(kDescriptionValue);
    }
}
