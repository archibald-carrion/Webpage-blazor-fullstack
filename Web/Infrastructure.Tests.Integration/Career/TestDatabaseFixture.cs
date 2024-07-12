using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Tests.Integration.Career;

public class SqlCareerRepositoryFixture
{
    private const string TestConnectionString = "your_connection_string";

    public ApplicationDbContext ApplicationDbContext { get; }

    public SqlCareerRepositoryFixture()
    {
        var dbContextOptionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbContextOptionBuilder.UseSqlServer(TestConnectionString);
        ApplicationDbContext = new(dbContextOptionBuilder.Options);
    }

    public static IEnumerable<Domain.Career.Entities.Career> SeedCareers()
    {
        yield return new Domain.Career.Entities.Career(
            Acronym.Create("CS"),
            LongName.Create("Computer Science"),
            LongName.Create("Computer things"),
            5,
            true,
            70.0,
            true);

        yield return new Domain.Career.Entities.Career(
            Acronym.Create("IG"),
            LongName.Create("Ingeniería Geológica"),
            LongName.Create("Geology things"),
            8,
            false,
            30.0,
            false);
    }

    public static IEnumerable<Contents> SeedContents()
    {
        yield return new Contents(
            Guid.NewGuid(),
            MediumName.Create("Redes"),
            LongName.Create("Introducción a las redes."),
            Acronym.Create("CS"),
            MediumName.Create("tecnológico"));

        yield return new Contents(
            Guid.NewGuid(),
            MediumName.Create("volcanologia"),
            LongName.Create("Introducción a los volcanes."),
            Acronym.Create("IG"),
            MediumName.Create("tecnológico"));
    }

    public async Task SeedDatabaseAsync()
    {
        // Delete all existing careers and contents
        await ApplicationDbContext.Contents.ExecuteDeleteAsync();
        await ApplicationDbContext.Careers.ExecuteDeleteAsync();

        // Re-seed the database
        await ApplicationDbContext.Careers.AddRangeAsync(SeedCareers());
        await ApplicationDbContext.Contents.AddRangeAsync(SeedContents());
        await ApplicationDbContext.SaveChangesAsync();
        ApplicationDbContext.ChangeTracker.Clear();
    }
}