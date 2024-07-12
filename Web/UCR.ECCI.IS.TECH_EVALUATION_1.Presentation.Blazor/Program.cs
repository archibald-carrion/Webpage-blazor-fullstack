using BlazorStrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UCR.ECCI.IS.TECH_EVALUATION_1.Presentation.Blazor;
using UCR.ECCI.IS.TECH_EVALUATION_1.Application;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazorStrap();
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddApplicationLayerServices();
builder.Services.AddInfrastructureApiClientLayerServices(builder.Configuration);

await builder.Build().RunAsync();
