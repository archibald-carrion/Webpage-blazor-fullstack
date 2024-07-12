using Xunit;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Tests.Integration.Career
{
    [CollectionDefinition("Database collection Careers")]
    public class DatabaseCollectionLS : ICollectionFixture<SqlCareerRepositoryFixture>
    {

    }
}