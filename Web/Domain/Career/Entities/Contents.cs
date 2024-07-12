using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

/// <summary>
/// Contents class is the main entity object of the project
/// </summary>
public class Contents
{
    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="id"></param>
    /// <param name="contentName"></param>
    /// <param name="description"></param>
    /// <param name="careerAcronym"></param>
    public Contents(Guid id, MediumName contentName, LongName description, Acronym careerAcronym, MediumName contentType)
    {
        // check if the id is empty, if it is throw an exception
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty");
        }
        else
        {
            Id = id;
        }

        ContentName = contentName;


        Description = description;

        CareerAcronym = careerAcronym;

        ContentType = contentType;
    }

    public Guid Id { get; }
    public MediumName ContentName { get; }
    public LongName Description { get; }
    public Acronym CareerAcronym { get; }
    public MediumName ContentType { get; }

    public Career? career { get; set; } = null!;


}
