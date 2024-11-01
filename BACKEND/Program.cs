using BACKEND.DTOs;
using BACKEND.Models;
using BACKEND.Services;
using BACKEND.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddSingleton<IPersonaServices, PersonaService2>();
builder.Services.AddKeyedSingleton<IPersonaServices, PersonaService>("personaservices");
builder.Services.AddKeyedSingleton<IPersonaServices, PersonaService2>("personaservices2");

builder.Services.AddKeyedSingleton<IRandomServices, RandomServices>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomServices, RandomServices>("randomScoped");
builder.Services.AddKeyedTransient<IRandomServices, RandomServices>("randomTransient");

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IBeerServicess, BeerService>();

builder.Services.AddHttpClient<IPostService, PostService>(
    c => c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]));

builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnections"));
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
