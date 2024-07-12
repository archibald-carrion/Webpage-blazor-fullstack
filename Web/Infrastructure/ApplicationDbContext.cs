using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure;

/// <summary>
/// ApplicationDbContext class inherits from DbContext
/// </summary>
public class ApplicationDbContext : DbContext
{
    public virtual DbSet<Domain.Career.Entities.Career> Careers { get; set; }
    public virtual DbSet<Domain.Career.Entities.Contents> Contents { get; set; }

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {

    }

    [Obsolete("Is only used for mocking/testing purposes. DO NOT USE in source code")]
    internal ApplicationDbContext()
    {

    }

    /// <summary>
    /// OnModelCreating method
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Configurations method used to configure the relations between the entities
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void CareerRelationConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Career.Entities.Career>()
            .HasMany(cr => cr.contents)
            .WithOne(ct => ct.career)
            .HasForeignKey(ct => ct.CareerAcronym)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
