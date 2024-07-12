using UCR.ECCI.IS.TECH_EVALUATION_1.Application;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application.Services.Interfaces;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Lifetimes:
// Singleton: The service is created once and reused for every request.
// Scoped: The service is created once per user session.
// Transient: The service is created every time it is requested.

builder.Services.AddApplicationLayerServices();
builder.Services.AddInfrastructureLayerServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// GetCareers service
app.MapGet("/listCareer", async (IListCareerService listCareerService) =>
{
    return await listCareerService.GetCareersAsync();
})
    .WithName("GetCareers")
    .WithOpenApi();

// GetContents service
app.MapGet("/listContents", async (IListContentsService listContentsService,
    string input) =>
{
    return await listContentsService.GetContentsAsync(input);
})
    .WithName("GetContents")
    .WithOpenApi();

// CreateContent service
app.MapGet("/CreateContent", async (ICreateContentService createContentService,
    string input) =>
{
    return await createContentService.PostCreateContentAsync(input);
})
    .WithName("CreateContent")
    .WithOpenApi();

// CreateCareer service
app.MapGet("/CreateCareer", async (ICreateCareerService createCareerService,
    string input) =>
{
    return await createCareerService.PostCreateCareerAsync(input);
})
    .WithName("CreateCareers")
    .WithOpenApi();

// DeleteContent service
app.MapDelete("/DeleteContent", async (ICreateContentService deleteContentService,
    Guid contentId) =>
{
    //var contentIdWrapper = GuidWrapper.Create(contentId);
    return await deleteContentService.DeleteContentAsync(contentId);
    //return await 
})
    .WithName("DeleteContent")
    .WithOpenApi();

app.MapGet("/GetSchoolarshipBudget", async (ICreateCareerService getScholarshipBudgetService,
       string careerAcronym) =>
{
    return await getScholarshipBudgetService.GetScholarshipBudgetAsync(careerAcronym);
})
    .WithName("GetScholarshipBudget")
    .WithOpenApi();

app.MapPost("/ModifyContent", async (ICreateContentService modifyContentService,
       string input) =>
{
    return await modifyContentService.PostModifyContentAsync(input);
})
    .WithName("ModifyContent")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}