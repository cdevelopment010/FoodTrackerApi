using FoodTrackerApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var foodLog = new List<LogEntry>(); 
app.MapGet("/", () => "Food Tracker API is live!");

app.MapPost("/log", (LogEntry entry) => 
{
    if (entry.Date == null) 
    {
        entry.Date = DateTime.Now;
    }
    foodLog.Add(entry);

    return Results.Ok("Logged");
});

app.MapGet("/log/week", () => 
{
    var start = DateTime.Today.AddDays(-6); 
    var week = foodLog 
        .Where(e => e.Date?.Date >= start)
        .OrderBy(e => e.Date)
        .ToList(); 

    return Results.Json(week);
});

app.Run();
