using DoctorWho.DB.Models;
using DoctorWho.DB.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DoctorWhoCoreDbContext>();
builder.Services.AddScoped<DoctorRepository>();
builder.Services.AddScoped<EpisodeRepository>();
builder.Services.AddScoped<EnemyRepository>();
builder.Services.AddScoped<CompanionRepository>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
