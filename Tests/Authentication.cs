using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Api.DTO;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class Authentication : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Authentication(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ServerShouldReturn_Succes_OnHealth_Endpoint()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task RegistrationEndpoint_Exists()
    {
        // Arrange
        // Noe å sende meldinger med
        var client = _factory.CreateClient();
        // Serveren å sende meldinger til
        // Vår hemmelige ID
        var id = new PasswordIdentifier { Secret = "some-secret" };

        // Act
        // Når vi sender en melding til endepunktet
        var response = await client.PostAsync(
            "/authentication/register",
            new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json"));

        // Assert
        // Resultat skal stemme overens med våre antakelser
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task RegistrationEndpoint_ShouldReturn_ValidJson()
    {
        // TODO! How to verify this?

        // Arrange
        var client = _factory.CreateClient();
        var id = new PasswordIdentifier { Secret = "some-secret" };

        // Act
        var response = await client.PostAsync(
            "/authentication/register",
            new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode(); // This ensures 2xx status code

        // Check that we have a valid JSON object
        var result = await response.Content.ReadFromJsonAsync<object>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RegistrationEndpoint_ShouldReturn_ValidLibraryCard()
    {
        // TODO! How to verify this?

        // Arrange
        var client = _factory.CreateClient();
        var id = new PasswordIdentifier { Secret = "some-secret" };
        var expected = new LibraryCard(id);

        // Act
        var response = await client.PostAsync(
            "/authentication/register",
            new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode(); // This ensures 2xx status code

        // Check that we have a valid JSON object
        var result = await response.Content.ReadFromJsonAsync<LibraryCardDTO>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task TryingToRegisterTwice_ShouldFail()
    {
        // TODO! How to verify this?

        // Arrange
        var client = _factory.CreateClient();
        var registrationAttemptA = new PasswordIdentifier { Secret = "some-secret" };
        var registrationAttemptB = new PasswordIdentifier { Secret = "some-secret" };

        // Act
        var responseA = await client.PostAsync(
            "/authentication/register",
            new StringContent(JsonSerializer.Serialize(registrationAttemptA), Encoding.UTF8, "application/json"));
        var responseB = await client.PostAsync(
            "/authentication/register",
            new StringContent(JsonSerializer.Serialize(registrationAttemptB), Encoding.UTF8, "application/json"));

        // Assert
        responseA.EnsureSuccessStatusCode(); // First should work
        Assert.Equal(HttpStatusCode.Conflict, responseB.StatusCode); // Second should fail
    }

    [Fact]
    public async Task TryingToRegisterTwice_ShouldFail_EvenAfter_Reboot()
    {
        // Arrange
        await using var factoryA = new WebApplicationFactory<Program>();
        var clientA = factoryA.CreateClient();

        var registration = new PasswordIdentifier { Secret = "some-secret" };
        var payload = new StringContent(
            JsonSerializer.Serialize(registration),
            Encoding.UTF8,
            "application/json");

        // Act 1: Register first time
        var responseA = await clientA.PostAsync("/authentication/register", payload);
        responseA.EnsureSuccessStatusCode();

        // "Reboot" server: dispose and recreate factory
        await using var factoryB = new WebApplicationFactory<Program>();
        var clientB = factoryB.CreateClient();

        // Act 2: Try registering again
        var responseB = await clientB.PostAsync("/authentication/register", payload);

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, responseB.StatusCode);
    }
}
