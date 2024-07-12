using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

/// <summary>
/// Career class is the main entity object of the project
/// </summary>
public class Career
{

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="acronym"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="duration"></param>
    public Career(Acronym acronym, LongName name, LongName description, int duration, bool isSteamRelated, double percentageFemaleStudents, bool isECCI)
    {
        Acronym = acronym;

        Name = name;

        Description = description;
        

        if (duration < 0)
        {
            throw new ArgumentException("Duration of career cannot be less than 0");
        }
        else
        {
            Duration = duration;
        }


        IsSteamRelated = isSteamRelated;

        // check if the percentage is negative, if it is throw an exception
        if (percentageFemaleStudents < 0)
        {
            throw new ArgumentException("Percentage cannot be negative");
        }
        else
        {
            PercentageFemaleStudents = percentageFemaleStudents;
        }

        IsECCI = isECCI;
    }

    public Acronym Acronym {  get; } // Acronym is a medium name valueObject
    public LongName Name { get;} // Name is a long name valueObject
    public LongName Description { get; } // Description is a long name valueObject 
    public int Duration { get; } // duration is a integer

    public bool IsSteamRelated { get; } // Indicates if the career is related to STEAM areas
    public double PercentageFemaleStudents { get; } // Percentage of female students
    public bool IsECCI { get; } // Indicates if the career is ecci

    public ICollection<Contents> contents { get; set; } = new List<Contents>();
}
