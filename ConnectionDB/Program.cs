using ConnectionDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("SQLiteConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
var app = builder.Build();

app.MapGet("/", (ApplicationContext context) => context.People.ToList());

app.Run();
