// Importação de dependências e do Entity Framework Core
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopApi.Repositories;
using WorkshopApi.Services;

var builder = WebApplication.CreateBuilder(args);


// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


// Repositories
builder.Services.AddScoped<WorkshopRepository>();
builder.Services.AddScoped<ColaboradorRepository>();
builder.Services.AddScoped<ParticipacaoRepository>();

// Services
builder.Services.AddScoped<WorkshopService>();
builder.Services.AddScoped<ColaboradorService>();

// Controllers
builder.Services.AddControllers();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// CORS
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

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.MapControllers();

app.Run();