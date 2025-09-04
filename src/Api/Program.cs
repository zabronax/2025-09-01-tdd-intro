using Api.DTO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/authentication/register", () =>
{
    var identifier = new PasswordIdentifier { Secret = "something" };
    var newLibraryCard = new LibraryCard(identifier);

    var returnDTO = new LibraryCardDTO
    {
        Id = newLibraryCard.Id.ToString()
    };

    return Results.Json(returnDTO);
});

app.MapGet("/health", () => "Ok");

app.Run();

// Required for using this in tests
public partial class Program { }
