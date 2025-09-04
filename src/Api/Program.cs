using Api.DTO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Dictionary<string, LibraryCard> registeredCards = [];

app.MapPost("/authentication/register", (PasswordIdentifier identifier) =>
{
    // First check if already registered
    if (registeredCards.ContainsKey(identifier.Secret))
    {
        return Results.Conflict();
    }

    var newLibraryCard = new LibraryCard(identifier);
    registeredCards.Add(identifier.Secret, newLibraryCard);

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
