using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project3.Data;
using Project3.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Entities>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("Flights")));
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.DescribeAllParametersInCamelCase();

    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "http://localhost:5200"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddScoped<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();

entities.Database.EnsureCreated();

var random = new Random();

if (!entities.Flights.Any())
{
    Flight[] flightsToSeed = new Flight[]
{
    new (Guid.NewGuid(), "S7", random.Next(10000, 30000).ToString(), new TimePlace("Moscow", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Novosibirsk", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "S7", random.Next(10000, 30000).ToString(), new TimePlace("Tomsk", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Omsk", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "AeroFlot", random.Next(10000, 30000).ToString(), new TimePlace("Kiiev", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Donbass", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "Pobeda", random.Next(10000, 30000).ToString(), new TimePlace("Vladimir", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Vladivostok", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "UralAirlines", random.Next(10000, 30000).ToString(), new TimePlace("Magadan", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Saint-Peterburg", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "Russia", random.Next(10000, 30000).ToString(), new TimePlace("Novosibirsk", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Bratsk", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "Russia", random.Next(10000, 30000).ToString(), new TimePlace("Barnaul", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Odessa", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "UralAirlines", random.Next(10000, 30000).ToString(), new TimePlace("Lugansk", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Krakow", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
            new(Guid.NewGuid(), "Pobeda", random.Next(10000, 30000).ToString(), new TimePlace("Warshava", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Sochi", DateTime.Now.AddHours(random.Next(4, 15))), random.Next(10, 80)),
};

    entities.Flights.AddRange(flightsToSeed);
    entities.SaveChanges();
}

app.UseCors(builder => builder.WithOrigins("*").
AllowAnyMethod().
AllowAnyHeader()
);

app.UseSwagger().UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
