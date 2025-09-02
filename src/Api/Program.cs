var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/authentication/register", () => "Ok");
app.MapGet("/health", () => "Ok");

app.Run();

// Required for using this in tests
public partial class Program { }
