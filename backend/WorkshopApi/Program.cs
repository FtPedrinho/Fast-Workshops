using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopApi.Repositories;
using WorkshopApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<WorkshopRepository>();
builder.Services.AddScoped<ColaboradorRepository>();
builder.Services.AddScoped<ParticipacaoRepository>();

builder.Services.AddScoped<WorkshopService>();
builder.Services.AddScoped<ColaboradorService>();
builder.Services.AddScoped<ParticipacaoService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("Frontend");

app.MapControllers();

app.Run();