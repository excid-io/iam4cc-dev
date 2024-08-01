using Excid.Registry.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddDbContext<RegistryDBContext>(options => options.UseSqlite("Data Source=db/registry.db"));

//https://stackoverflow.com/questions/62475109/asp-net-core-jwt-authentication-changes-claims-sub
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["JwtBearer:ValidIssuer"];
    options.Audience = builder.Configuration["JwtBearer:ValidAudience"];
    options.RequireHttpsMetadata = false;
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
