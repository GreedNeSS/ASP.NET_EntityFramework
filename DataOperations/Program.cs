using DataOperations.DBContext;
using DataOperations.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("SQLite");
builder.Services.AddDbContext<ApplicationContext>(opts => opts.UseSqlite(connection));
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", async (ApplicationContext db) => await db.People.ToListAsync());

app.MapGet("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    Person? person = await db.People.FirstOrDefaultAsync(p => p.Id == id);

    if (person is null)
    {
        return Results.NotFound(new { message = "Пользователь не найден!" });
    }

    return Results.Json(person);
});

app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    Person? person = await db.People.FirstOrDefaultAsync(p => p.Id == id);

    if (person is null)
    {
        return Results.NotFound(new { message = "Пользователь не найден!" });
    }

    db.People.Remove(person);
    await db.SaveChangesAsync();
    return Results.Json(person);
});

app.MapPost("/api/users", async (Person dataPerson,ApplicationContext db) =>
{
    await db.People.AddAsync(dataPerson);
    await db.SaveChangesAsync();
    return Results.Json(dataPerson);
});

app.MapPut("/api/users", async (Person dataPerson, ApplicationContext db) =>
{
    Person? person = await db.People.FirstOrDefaultAsync(p => p.Id == dataPerson.Id);

    if (person is null)
    {
        return Results.NotFound(new { message = "Пользователь не найден!" });
    }

    person.Name = dataPerson.Name;
    person.Age = dataPerson.Age;
    await db.SaveChangesAsync();
    return Results.Json(person);
});

app.Run();
