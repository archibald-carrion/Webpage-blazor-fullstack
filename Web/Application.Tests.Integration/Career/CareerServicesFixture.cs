using Moq;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Application.Tests.Integration.Career;


public class CareerServicesFixture : IDisposable
{
    public Mock<ICareerRepository> MockCareerRepository { get; private set; }

    public CareerServicesFixture()
    {
        MockCareerRepository = new Mock<ICareerRepository>();
    }

    public void Dispose()
    {
        // Clean up any resources if needed
    }
}