using EvertecPruebas.Api.Filters;
using EvertecPruebas.DataAcces;
using EvertecPruebas.DataAcces.Interfaces;
using EvertecPruebas.Repository.Handlers;
using EvertecPruebas.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string? SwName = Assembly.GetCallingAssembly().GetName().Name;
string? xmlFileName = Assembly.GetExecutingAssembly().GetName().Name;
string? Connection = builder.Configuration.GetConnectionString("Connection");
string cors = builder.Configuration["Allowedhosts"] ?? "*";
builder.Services.AddCors(op => op.AddDefaultPolicy(builder =>
  builder.WithOrigins(cors)
  .AllowAnyHeader()
  .AllowAnyMethod()
    ));
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = $"{SwName}", Version = "V1" });
    var xmlFile = $"{xmlFileName}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
    config.SchemaFilter<SwaggerFilter>();
    config.EnableAnnotations();
});
builder.Services.AddDbContext<AppDBContext>(options =>
                                     options.UseSqlServer(Connection));
builder.Services.AddScoped<IDbContext>(provider => provider.GetRequiredService<AppDBContext>());
builder.Services.AddScoped<IUsuario, UsuarioHandler>();
builder.Services.AddScoped<IRepository, RepositoryHandler>();
builder.Services.AddScoped<IEstadoCivil, EstadoCivilHandler>();
var app = builder.Build();
app.UseMiddleware<ExceptionFilter>();
// Configure the HTTP request pipeline.
app.UseCors();
app.UseSwagger();
app.UseRouting();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
