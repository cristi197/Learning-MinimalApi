using LearningMinimalApi.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<GuideGenerator>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!");

app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
{
    if(searchTerm is null)
    {
        return Results.NotFound();
    }

    var results = peopleService.Search(searchTerm);
    return Results.Ok(results);
});

app.MapGet("mix/{routeParam}", (string routeParam, int queryParam, GuideGenerator guidGenerator) =>
{
    return $"{routeParam} {queryParam} {guidGenerator.NewGuid}";
});

app.Run();
