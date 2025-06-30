using Microsoft.EntityFrameworkCore;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Services;
using ShowTime.Components;
using ShowTime.DataAccess;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var conenectionString = builder.Configuration.GetConnectionString("ShowTimeContext");
builder.Services.AddDbContext<ShowTimeDbContext>(options =>
    options.UseSqlServer(conenectionString));

builder.Services.AddTransient<IRepository<Artist>, BaseRepository<Artist>>();
builder.Services.AddTransient<IArtistService, ArtistService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
