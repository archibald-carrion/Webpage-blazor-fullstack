using Newtonsoft.Json.Linq;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.ValueObjects;
using Microsoft.EntityFrameworkCore;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;
using Guid = System.Guid;
using System.Text.Json;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Career.Repositories;

/// <summary>
/// SqlCareerRepository class inherits from ICareerRepository
/// </summary>
internal class SqlCareerRepository : ICareerRepository
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Constructor of the class
    /// </summary>
    /// <param name="dbContext"></param>
    public SqlCareerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// PostCreateCareerAsync method
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<bool> PostCreateCareerAsync(string input)
    {
        try
        {
            JObject? obj = JObject.Parse(input);

            // check if obj is null
            if (obj == null)
            {
                return false;
            }

            string careerAcronym = (string?)obj["Acronym"]?? string.Empty; // Acronym
            string careerDescription = (string?)obj["Description"] ?? string.Empty; // Description
            string careerName = (string?)obj["Name"] ?? string.Empty; // Name
            int careerDuration = (int?)obj["Duration"] ?? 0; // Duration
            bool isSTEAMRelated = (bool?)obj["isSteamRelated"] ?? false; // IsSTEAMRelated
            double PercentageFemaleStudents = (double?)obj["PercentageFemaleStudents"] ?? 0; // PercentageFemaleStudents
            bool isECCI = (bool?)obj["isECCI"] ?? false; // IsECCI

            //// create a new career
            Domain.Career.Entities.Career newCareer = new(
                Acronym.Create(careerAcronym), 
                LongName.Create(careerName), 
                LongName.Create(careerDescription), 
                careerDuration,
                isSTEAMRelated,
                PercentageFemaleStudents,
                isECCI);

            _dbContext.Careers.Add(newCareer); // TODO: implement Careers of _dBcontext so that it can be added

            //// Wait to add the new career
            await _dbContext.SaveChangesAsync();  // TODO: implement Careers of _dBcontext so that it can be added
        }
        catch (DbUpdateException ex)
        {
            // Handle specific database exceptions
            // For example, unique constraint violations
            // ex.InnerException may contain more detailed information
            Console.WriteLine($"Database error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }

        return true; //borrar
    }

    /// <summary>
    /// GetCareersAsync method
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Domain.Career.Entities.Career>> GetCareersAsync()
    {
        return await _dbContext
            .Careers
            .ToListAsync();
    }

    /// <summary>
    /// GetContentsAsync method
    /// </summary>
    /// <param name="careerAcronym"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Domain.Career.Entities.Contents>> GetContentsAsync(string careerAcronym)
    {
        var contents = _dbContext
            .Contents
            .AsEnumerable()
            .Where(c => c.CareerAcronym.Value == careerAcronym);
            //.FromSqlRaw("SELECT * FROM Contents WHERE CareerAcronym = {0}", careerAcronym)
            //.ToListAsync();  // Convierte los resultados a una lista

        return contents;
    }

    /// <summary>
    /// PostCreateContentAsync method
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<bool> PostCreateContentAsync(string input)
    {
        try
        {
            JObject? obj = JObject.Parse(input);

            // check if obj is null
            if (obj == null)
            {
                return false;
            }

            string contentName = (string?)obj["ContentName"] ?? string.Empty; // Acronym
            string description = (string?)obj["Description"] ?? string.Empty; // Description
            string careerAcronym = (string?)obj["CareerAcronym"] ?? string.Empty; // Name
            string contentType = (string?)obj["ContentType"] ?? string.Empty; // Duration

            //// create a new career
            Contents newContent = new Contents(
                Guid.NewGuid(),
                MediumName.Create(contentName),
                LongName.Create(description),
                Acronym.Create(careerAcronym),
                MediumName.Create(contentType)
                );
          
            _dbContext.Contents.Add(newContent); // TODO: implement Careers of _dBcontext so that it can be added
            //// Wait to add the new career
            await _dbContext.SaveChangesAsync();  // TODO: implement Careers of _dBcontext so that it can be added
        }
        catch (DbUpdateException ex)
        {
            // Handle specific database exceptions
            // For example, unique constraint violations
            // ex.InnerException may contain more detailed information
            Console.WriteLine($"Database error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }

        return true; //borrar
    }

    public async Task<bool> DeleteContentAsync(System.Guid contentId)
    {
        try
        {
            var content = await _dbContext.Contents.FindAsync(contentId);

            if (content == null)
            {
                return false;
            }

            _dbContext.Contents.Remove(content);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting content: {ex.Message}");
            return false;
        }
    }

    public async Task<double> GetScholarshipBudgetAsync(string careerAcronym)
    {
        // Fetch the career details
        var career = await _dbContext.Careers
            .FindAsync(Acronym.Create(careerAcronym));

        if (career == null)
        {
            // Handle the case where the career is not found
            //search but with string instead of Acronym
            career = await _dbContext.Careers.Where(c => c.Acronym.Value == careerAcronym).FirstOrDefaultAsync();
        }

        // print career details
        //Console.WriteLine($"Career: {career.Name.Value}");


        // Fetch the contents related to the career
        var contents = _dbContext
           .Contents
           .AsEnumerable()
           .Where(c => c.CareerAcronym.Value == careerAcronym); // used for unit testing
           // .FromSqlRaw("SELECT * FROM Contents WHERE CareerAcronym = {0}", careerAcronym)
           //.ToListAsync();  // Convierte los resultados a una lista

        // check if contents is not null

        //if (career == null)
        //{
        //    throw new ArgumentException($"Career with acronym {careerAcronym} not found.");
        //}

        // Calculate base amount
        double baseAmount = 0;

        if(contents != null)
        {
            foreach (var content in contents)
            {
                // print the name of the content
                // Console.WriteLine($"Content: {content.ContentName.Value}");

                if (content.ContentType.Value == "tecnológico" || content.ContentType.Value == "ambiental" || content.ContentType.Value == "social")
                {
                    baseAmount += 100;
                }
                if (content.ContentType.Value == "tecnológico")
                {
                    baseAmount += 100;
                }
            }
        }

        // Additional donations based on STEAM relation
        double additionalPercentage;
        if (career.IsSteamRelated)
        {
            additionalPercentage = 0.5;
        }
        else
        {
            additionalPercentage = 0.2;
        }

        double donationAmount = baseAmount + (baseAmount * additionalPercentage);

        // Additional 10% if the career is STEAM-related
        if (career.IsSteamRelated)
        {
            donationAmount += donationAmount * 0.1;
        }

        // Additional donation based on the percentage of female students
        if (career.PercentageFemaleStudents > 50)
        {
            if (career.IsSteamRelated)
            {
                donationAmount += donationAmount * 0.08;
            }
            else
            {
                donationAmount += baseAmount * 0.1;
            }
        }

        // Additional 5% if the career is from the Computer Science and Informatics area
        if (career.IsECCI)
        {
            donationAmount += donationAmount * 0.05;
        }

        return donationAmount;
    }

    public async Task<bool> PostModifyContentAsync(string input)
    {
        try
        {
            JObject obj;

            // Try parsing as array first
            if (input.TrimStart().StartsWith("["))
            {
                JArray array = JArray.Parse(input);
                if (array == null || !array.Any())
                {
                    return false;
                }
                obj = (JObject)array[0];
            }
            else
            {
                // If not an array, parse as single object
                obj = JObject.Parse(input);
            }

            Guid contentId = (Guid?)obj["Id"] ?? Guid.Empty;
            string contentName = (string?)obj["ContentName"] ?? string.Empty;
            string description = (string?)obj["Description"] ?? string.Empty;
            string careerAcronym = (string?)obj["CareerAcronym"] ?? string.Empty;
            string contentType = (string?)obj["ContentType"] ?? string.Empty;

            // Fetch the content from the database
            var content = await _dbContext.Contents.FindAsync(contentId);

            // Check if content exists
            if (content == null)
            {
                Console.WriteLine($"Content with ID {contentId} not found.");
                return false;
            }

            // Delete the existing content
            _dbContext.Contents.Remove(content);

            // Create a new content with updated values
            Contents newContent = new Contents(
                contentId,
                MediumName.Create(contentName),
                LongName.Create(description),
                Acronym.Create(careerAcronym),
                MediumName.Create(contentType)
            );

            // Add the new content to the database
            _dbContext.Contents.Add(newContent);

            // Save changes
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }



}
