using Area52Horeca.Interfaces;
using Area52Horeca.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReserveringRepository, ReserveringRepository>();
builder.Services.AddScoped<ITijdslotRepository, TijdslotRepository>();
builder.Services.AddScoped<ITafelRepository, TafelRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();