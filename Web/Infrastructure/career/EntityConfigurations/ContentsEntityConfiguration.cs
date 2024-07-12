using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.career.EntityConfigurations;

/// <summary>
/// Contents entity configuration class inherits from IEntityTypeConfiguration
/// </summary>
internal class ContentsEntityConfiguration : IEntityTypeConfiguration<Domain.Career.Entities.Contents>
{

    /// <summary>
    /// Configure the entity Contents
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Domain.Career.Entities.Contents> builder)
    {
        builder.ToTable("Contents");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ContentName)
            .IsRequired()
            .HasMaxLength(MediumName.MaxLenght)
            .HasConversion(
                // C# => SQL conversion
                convertToProviderExpression: valueObject => valueObject.Value,
                // SQL => C#
                convertFromProviderExpression: acronymString => MediumName.Create(acronymString));

        builder.Property(c => c.CareerAcronym)
            .IsRequired()
            .HasMaxLength(Acronym.MaxLenght)
            .HasConversion(
                // C# => SQL conversion
                convertToProviderExpression: valueObject => valueObject.Value,
                // SQL => C#
                convertFromProviderExpression: acronymString => Acronym.Create(acronymString));

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(LongName.MaxLenght)
            .HasConversion(
                // C# => SQL conversion
                convertToProviderExpression: valueObject => valueObject.Value,
                // SQL => C#
                convertFromProviderExpression: longString => LongName.Create(longString));

        builder.Property(c => c.ContentType)
            .IsRequired()
            .HasMaxLength(MediumName.MaxLenght)
            .HasConversion(
                // C# => SQL conversion
                convertToProviderExpression: valueObject => valueObject.Value,
               // SQL => C#
               convertFromProviderExpression: mediumString => MediumName.Create(mediumString));

        //builder.Property(c => c.Id)
        //    .IsRequired();



    }
}
