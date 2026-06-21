using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Backend_Area42_3;
using Backend_Area42_3.DTO.Output;
using System.Net;

namespace Tests;

public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    private readonly string runnerId = Guid.NewGuid().ToString();
    private readonly string baseUrl = "http://localhost:5000/api/auth";

    public AuthTests(WebApplicationFactory<Program> factory)
    {
        Environment.SetEnvironmentVariable("JwtSecretKey", "test-secret-key-has-to-be-32-characters-long-or-it-will-not-work");
        client = factory.CreateClient();
    }

    private string CreateEmail(string prefix)
    {
        return $"{prefix}.{runnerId}@example.com";
    }

    [Fact]
    public async Task Register_Returns_Ok()
    {
        var request = new
        {
            Name = Helpers.GenerateRandomName(),
            Phone = Helpers.GenerateRandomPhoneNumber(),
            Email = CreateEmail("register-ok"),
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

    [Fact]
    public async Task Register_Returns_BadRequest_When_MissingFields()
    {
        var request = new
        {
            Email = CreateEmail("register-missing-fields"),
            Password = Helpers.GenerateRandomPassword()
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/register",
            request
        );

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_Returns_BadRequest_When_DuplicateEmail()
    {
        string email = CreateEmail("register-duplicate");

        var firstRequest = new
        {
            Name = Helpers.GenerateRandomName(),
            Phone = Helpers.GenerateRandomPhoneNumber(),
            Email = email,
            Password = Helpers.GenerateRandomPassword()
        };

        var firstResponse = await client.PostAsJsonAsync(
            $"{baseUrl}/register",
            firstRequest
        );

        Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);

        var duplicateRequest = new
        {
            Name = Helpers.GenerateRandomName(),
            Phone = Helpers.GenerateRandomPhoneNumber(),
            Email = email,
            Password = Helpers.GenerateRandomPassword()
        };

        var duplicateResponse = await client.PostAsJsonAsync(
            $"{baseUrl}/register",
            duplicateRequest
        );

        Assert.Equal(HttpStatusCode.BadRequest, duplicateResponse.StatusCode);
    }

    [Fact]
    public async Task Register_Returns_BadRequest_When_InvalidEmailFormat()
    {
        var request = new
        {
            Name = Helpers.GenerateRandomName(),
            Phone = Helpers.GenerateRandomPhoneNumber(),
            Email = "not-an-email-address",
            Password = Helpers.GenerateRandomPassword()
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/register",
            request
        );

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}