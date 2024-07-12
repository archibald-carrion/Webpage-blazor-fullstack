using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Tests.Unit.Services;

public class CareerServiceFixture
{
    public IEnumerable<Career> Careers { get; }
    public IEnumerable<Contents> Contents { get; }

    public CareerServiceFixture()
    {
        // list of valid careers
        Careers = new List<Career>
        {
            new Career(
                Acronym.Create("CS"),
                LongName.Create("Computer Science"),
                LongName.Create("Computer Science descrp."),
                4,
                true,
                30.5,
                true),
            new Career(
                Acronym.Create("EE"),
                LongName.Create("Electrical Engineering"),
                LongName.Create("Electrical Engineering descrp."),
                5,
                true,
                25.0,
                false)
        };

        // list of valid contents
        Contents = new List<Contents>
        {
            new Contents(
                Guid.NewGuid(),
                MediumName.Create("Programming"),
                LongName.Create("Introduction to Programming"),
                Acronym.Create("EE"),
                MediumName.Create("tecnológico")),
            new Contents(
                Guid.NewGuid(),
                MediumName.Create("Circuits"),
                LongName.Create("Basic Circuits"),
                Acronym.Create("EE"),
                MediumName.Create("tecnológico"))
        };
    }
}