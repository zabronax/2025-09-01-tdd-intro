var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/authentication/register", () =>
{
  var identifier = new PasswordIdentifier { Secret = "something" };
  var newLibraryCard = new LibraryCard(identifier);
  return Results.Json<LibraryCard>(newLibraryCard);
});

app.MapGet("/health", () => "Ok");

app.Run();

// Required for using this in tests
public partial class Program { }
