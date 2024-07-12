using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Tests.Unit.Career.Repositories;

public class SqlCareerRepositoryTestsFixture
{
    public Guid InvalidId { get; private set; }
    public Domain.Career.Entities.Career CareerValid { get; private set; }
    public IEnumerable<Domain.Career.Entities.Career> CareersList { get; private set; }

    public IEnumerable<Domain.Career.Entities.Career> EmptyCareerList { get; private set; }
    public IEnumerable<Contents> ContentsList { get; private set; }

    public SqlCareerRepositoryTestsFixture()
    {
        InvalidId = Guid.Empty;
        CareerValid = new Domain.Career.Entities.Career(
            Acronym.Create("CS"),
            LongName.Create("Computer Science"),
            LongName.Create("Study of computation"),
            4,
            true,
            30.5,
            true
        );

        // create an empty career list
        EmptyCareerList = new List<Domain.Career.Entities.Career>();


        CareersList = new List<Domain.Career.Entities.Career>
            {
                
                new Domain.Career.Entities.Career(
                    Acronym.Create("EE"),
                    LongName.Create("Electrical Engineering"),
                    LongName.Create("Study of electrical systems"),
                    4,
                    true,
                    25.0,
                    false
                ),
                CareerValid
            };

        ContentsList = new List<Contents>
            {
                new Contents(
                    Guid.NewGuid(),
                    MediumName.Create("Programming"),
                    LongName.Create("Introduction to programming"),
                    Acronym.Create("CS"),
                    MediumName.Create("Course")
                ),
                new Contents(
                    Guid.NewGuid(),
                    MediumName.Create("Circuits"),
                    LongName.Create("Basic electrical circuits"),
                    Acronym.Create("EE"),
                    MediumName.Create("Course")
                ),
                new Contents(
                    Guid.NewGuid(),
                    MediumName.Create("Algorithms"),
                    LongName.Create("Introduction to algorithms"),
                    Acronym.Create("EE"),
                    MediumName.Create("tecnológico")
                )
            };


    }
}