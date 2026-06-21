using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Backend_Area42_3;
using Backend_Area42_3.DTO.Output;
using System.Net;

namespace Tests;

public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    private readonly string baseUrl = "http://localhost:5000/api/auth";

    public AuthTests(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("JwtSecretKey", "test-secret-key-has-to-be-32-characters-long-or-it-will-not-work");
        client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_Returns_Ok()
    {
        var request = new
        {
            Name = Helpers.GenerateRandomName(),
            Phone = Helpers.GenerateRandomPhoneNumber(),
            Email = Helpers.GenerateRandomEmail(),
            Password = Helpers.GenerateRandomPassword()
        };

        var response = await client.PostAsJsonAsync(
                    $"{baseUrl}/register",
                    request
                );

        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(@"{""success"":true,""message"":""Registratie geslaagd.""}", body);
    }
}