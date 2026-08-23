var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!");

app.MapGet("/add", (int a, int b) => $"{a} + {b} = {a + b}");

app.MapPost("/echo", (Todo todo) => todo);

app.Run();

public record Todo(int Id, string Title, bool Done);
