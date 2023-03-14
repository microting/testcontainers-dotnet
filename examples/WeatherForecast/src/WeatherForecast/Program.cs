﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;
using WeatherForecast;
using WeatherForecast.Contexts;
using WeatherForecast.Interactors.SearchCityOrZipCode;
using WeatherForecast.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
  // No database connection string is available. Start and seed a database using Testcontainers for .NET first.
  builder.Services.AddSingleton<DatabaseContainer>();
  builder.Services.AddHostedService(services => services.GetRequiredService<DatabaseContainer>());
  builder.Services.AddDbContext<WeatherDataContext>((services, options) =>
  {
    var databaseContainer = services.GetRequiredService<DatabaseContainer>();
    options.UseSqlServer(databaseContainer.GetConnectionString());
  });
}
else
{
  builder.Services.AddDbContext<WeatherDataContext>((_, options) => options.UseSqlServer(connectionString));
}

builder.Services.AddScoped<IWeatherDataReadOnlyRepository, WeatherDataReadOnlyContext>();
builder.Services.AddScoped<IWeatherDataWriteOnlyRepository, WeatherDataWriteOnlyContext>();
builder.Services.AddScoped<ISearchCityOrZipCode, SearchCityOrZipCode>();

var app = builder.Build();
app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseEndpoints(endpoint => endpoint.MapControllers());
app.UseEndpoints(endpoint => endpoint.MapRazorPages());
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

public sealed partial class Program
{
}
