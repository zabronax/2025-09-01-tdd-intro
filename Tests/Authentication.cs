using System.Net;
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

    // [Fact]
    // public void Register_Endpoint_Should_Allow_Registering_New_ID()
    // {
    //     // Arrange
    //     // Noe å sende meldinger med
    //     var client = new HttpClient();
    //     // Serveren å sende meldinger til
    //     // Vår hemmelige ID
    //     string userSecret = "super-secret-password";
    //     var payload = new { message = userSecret };
    //     // Det "ID kort" (JWT) vi forventer å få tilbake 

    //     // Act
    //     // Når vi sender en melding til endepunktet
    //     // client.PostAsync(
    //     //     "/authentication/register",
    //     //     new StringContent(payload.ToString(), Encoding.UTF8, "application/json"));
    //     // Så får vi tilbake et result

    //     // Assert
    //     // Resultat skal stemme overens med våre antakelser
    // }
}
