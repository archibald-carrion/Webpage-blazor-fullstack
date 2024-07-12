using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Tests.Unit.Career.Entities;

public class ContentsValueObjectsFixture
{
    private const string kContentNameValue = "Intro to Programming"; // Correct content name
    private const string kDescriptionValue = "Introduction to programming."; // Correct description
    private const string kCareerAcronymValue = "CS"; // Correct career acronym
    private const string kContentTypeValue = "Technical"; // Correct content type

    public MediumName ContentName { get; set; }
    public LongName Description { get; set; }
    public Acronym CareerAcronym { get; set; }
    public MediumName ContentType { get; set; }

    /// <summary>
    /// Constructor that initializes the fixture.
    /// </summary>
    public ContentsValueObjectsFixture()
    {
        ContentName = MediumName.Create(kContentNameValue);
        Description = LongName.Create(kDescriptionValue);
        CareerAcronym = Acronym.Create(kCareerAcronymValue);
        ContentType = MediumName.Create(kContentTypeValue);
    }
}
