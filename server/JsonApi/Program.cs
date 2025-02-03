using JsonApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll", policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
  );
});

var connString = builder.Configuration.GetConnectionString("App");
builder.Services.AddSqlite<AppDbContext>(connString);

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

await app.MigrateDbAsync();

app.Run();
