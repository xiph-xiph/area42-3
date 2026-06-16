using Backend_Area42_3.Repositories;
using Backend_Area42_3.Services;
using DotNetEnv;
using Npgsql;

namespace Backend_Area42_3;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        ConfigureDependencyInjection(builder.Services);

        var app = builder.Build();

        app.MapControllers();

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
        services.AddScoped<AuthService>();
    }
}
