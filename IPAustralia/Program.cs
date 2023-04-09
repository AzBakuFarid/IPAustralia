using AutoMapper;
using IPAustralia.Models;
using IPAustralia.ServiceAbstractions;
using IPAustralia.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
    cfg.DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;
    cfg.AddMaps(Assembly.GetExecutingAssembly());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<IApiCaller, ApiCaller>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddSingleton<IScrapper, Scrapper>();
builder.Services.Configure<Config>(builder.Configuration.GetSection("Configs"));



var app = builder.Build();


app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();
