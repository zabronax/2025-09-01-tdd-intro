using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
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
    public async Task Register_Endpoint_Should_Allow_Registering_New_ID()
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
        response.EnsureSuccessStatusCode();
        // Resultat skal stemme overens med våre antakelser
    }
}
