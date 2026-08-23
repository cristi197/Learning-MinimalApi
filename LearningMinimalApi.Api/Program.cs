using LearningMinimalApi.Api;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<GuideGenerator>();

var app = builder.Build();

// Middleware registration start here

app.UseMiddleware<CookiePolicyMiddleware>();

app.MapGet("/", () => "Hello World!");

app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!");

app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
{
    if (searchTerm is null)
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

app.MapGet("httpcontext-1", async context =>
{
    await context.Response.WriteAsync("Hello from httpcontext-1");
});

app.MapGet("httpcontext-2", async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello from httpcontext-1");
});

app.MapGet("http", async (HttpRequest httpsRequest, HttpResponse httpResponse) =>
{
    var queries = httpsRequest.QueryString.Value;
    await httpResponse.WriteAsync($"Hello from http response. Queries were: {queries}");
});

app.MapGet("map-point", (MapPoint? point) =>
{

    return Results.Ok(point);

});

app.MapPost("map-point", (MapPoint point) =>
{
    return Results.Ok(point);
});

app.Run();
