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

    [Fact]
    public async Task Register_Returns_BadRequest_When_WeakPassword()
    {
        var request = new
        {
            Name = Helpers.GenerateRandomName(),
            Phone = Helpers.GenerateRandomPhoneNumber(),
            Email = CreateEmail("register-weak-password"),
            Password = "123"
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/register",
            request
        );

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Returns_Ok_With_Valid_Credentials()
    {
        string email = CreateEmail("login-ok");
        string password = Helpers.GenerateRandomPassword();

        await RegisterUserAsync(email, password);

        var request = new
        {
            Email = email,
            Password = password
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/login",
            request
        );

        var body = await response.Content.ReadFromJsonAsync<TokenDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.True(body!.Success);
        Assert.Equal("Inloggen geslaagd.", body.Message);
        Assert.False(string.IsNullOrWhiteSpace(body.Token));
        Assert.False(string.IsNullOrWhiteSpace(body.ValidUntil));
    }

    [Fact]
    public async Task Login_Returns_BadRequest_When_MissingFields()
    {
        var request = new
        {
            Email = CreateEmail("login-missing-fields")
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/login",
            request
        );

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Returns_Unauthorized_When_IncorrectPassword()
    {
        string email = CreateEmail("login-incorrect-password");
        string password = Helpers.GenerateRandomPassword();

        await RegisterUserAsync(email, password);

        var request = new
        {
            Email = email,
            Password = $"{password}-wrong"
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/login",
            request
        );

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_Returns_Unauthorized_When_NonExistentEmail()
    {
        var request = new
        {
            Email = CreateEmail("login-missing-user"),
            Password = Helpers.GenerateRandomPassword()
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/login",
            request
        );

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private async Task RegisterUserAsync(string email, string password)
    {
        var request = new
        {
            Name = Helpers.GenerateRandomName(),
            Phone = Helpers.GenerateRandomPhoneNumber(),
            Email = email,
            Password = password
        };

        var response = await client.PostAsJsonAsync(
            $"{baseUrl}/register",
            request
        );

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}