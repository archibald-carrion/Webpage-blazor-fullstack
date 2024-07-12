using Riok.Mapperly.Abstractions;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Career.Dtos;


[Mapper]

internal static partial class ContentsDtoMapperz
{
    internal static partial Domain.Career.Entities.Contents ToEntity(Client.Models.Contents CareerDto);

    /// <summary>
    /// Maps LongNameDto to LongName
    /// </summary>
    /// <param name="longNameDto"></param>
    /// <returns></returns>
    internal static LongName ToValueObject(Client.Models.LongName longNameDto)
    {
        if (longNameDto == null || longNameDto.Value == null)
        {
            return LongName.Invalid;
        }
        return LongName.Create(longNameDto.Value);
    }

    /// <summary>
    /// Maps a MediumNameDto to a MediumName
    /// </summary>
    /// <param name="mediumNameDto"></param>
    /// <returns></returns>
    internal static MediumName ToValueObject(Client.Models.MediumName mediumNameDto)
    {
        if (mediumNameDto == null || mediumNameDto.Value == null)
        {
            return MediumName.Invalid;
        }
        return MediumName.Create(mediumNameDto.Value);
    }

    /// <summary>
    /// Maps a AcronymDto to a Acronym
    /// </summary>
    /// <param name="AcronymDto"></param>
    /// <returns></returns>
    internal static Acronym ToValueObject(Client.Models.Acronym AcronymDto)
    {
        if (AcronymDto == null || AcronymDto.Value == null)
        {
            return Acronym.Invalid;
        }
        return Acronym.Create(AcronymDto.Value);
    }
}
