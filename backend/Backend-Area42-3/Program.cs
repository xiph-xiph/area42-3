using Backend_Area42_3.Services;

namespace Backend_Area42_3;

public class Program
{
    public static async Task Main(string[] args)
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
        services.AddScoped<AuthService>();
    }
}
