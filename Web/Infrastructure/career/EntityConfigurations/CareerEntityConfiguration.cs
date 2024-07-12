using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Career.EntityConfigurations;

/// <summary>
/// Career entity configuration class inherits from IEntityTypeConfiguration
/// </summary>
internal class CareerEntityConfiguration : IEntityTypeConfiguration<Domain.Career.Entities.Career>
{

    /// <summary>
    /// Configure the entity Career
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Domain.Career.Entities.Career> builder)
    {
        builder.ToTable("Career");
        builder.Property(c => c.Duration)
            .IsRequired();
        builder.HasKey(c => c.Acronym);


        builder.Property(c => c.Acronym)
            .IsRequired()
            .HasMaxLength(Acronym.MaxLenght)
            .HasConversion(
                // C# => SQL conversion
                convertToProviderExpression: valueObject => valueObject.Value,
                // SQL => C#
                convertFromProviderExpression: acronymString => Acronym.Create(acronymString));

        builder.Property(c => c.Name)
           .IsRequired()
           .HasMaxLength(LongName.MaxLenght)
           .HasConversion(
               // C# => SQL conversion
               convertToProviderExpression: valueObject => valueObject.Value,
               // SQL => C#
               convertFromProviderExpression: nameString => LongName.Create(nameString));

        builder.Property(c => c.Description)
           .IsRequired()
           .HasMaxLength(LongName.MaxLenght)
           .HasConversion(
               // C# => SQL conversion
               convertToProviderExpression: valueObject => valueObject.Value,
               // SQL => C#
               convertFromProviderExpression: descriptionString => LongName.Create(descriptionString));

        builder.Property(c => c.IsSteamRelated)
            .IsRequired();

        builder.Property(c => c.PercentageFemaleStudents)
            .IsRequired();

        builder.Property(c => c.IsECCI)
            .IsRequired();
    }
}
