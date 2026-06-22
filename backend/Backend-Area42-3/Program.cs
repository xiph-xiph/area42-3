using Backend_Area42_3.Repositories;
using Backend_Area42_3.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace Backend_Area42_3;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
            .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(
                            Environment.GetEnvironmentVariable("JwtSecretKey")
                                ?? throw new InvalidOperationException(
                                    "JwtSecretKey environment variable is not set"
                                )
                        )
                    ),
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddControllers();

        ConfigureDependencyInjection(builder.Services);

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapControllers();

        app.MapFallbackToFile("index.html");

        app.Run();
    }

    private static void ConfigureDependencyInjection(IServiceCollection services)
    {
        Env.Load();
        var csb = new NpgsqlConnectionStringBuilder
        {
            Host = Environment.GetEnvironmentVariable("PostgresHost") ?? "localhost",
            Database = Environment.GetEnvironmentVariable("PostgresDatabase") ?? "area423",
            Username = Environment.GetEnvironmentVariable("PostgresUsername") ?? "postgres",
            Password = Environment.GetEnvironmentVariable("PostgresPassword") ?? "postgres",
        };
        services.AddSingleton(NpgsqlDataSource.Create(csb.ConnectionString));
        services.AddSingleton<IPasswordHasher, Argon2PasswordHasher>();
        services.AddSingleton<ITokenGenerator, JWTTokenGenerator>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<AuthService>();
        services.AddScoped<MenuService>();
        services.AddScoped<OrderService>();
        services.AddScoped<IssueService>();
    }
}
