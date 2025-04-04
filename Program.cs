using FoodTrackerApi.Models;
using FoodTrackerApi.Services;
using FoodTrackerApi.Utils;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILogSummaryService, LogSummaryService>(); 

var app = builder.Build();

var foodLog = new List<LogEntry>(); 
app.MapGet("/", () => "Food Tracker API is live!");

app.MapPost("/log", (LogEntry entry) => 
{
    if (entry.Date == null) 
    {
        entry.Date = DateTime.Now;
    }

    if (!Enum.IsDefined(typeof(FoodStatus), entry.Status))
    {
        return Results.BadRequest("Invalid status value.");
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

app.MapGet("/log/summary/{year:int}/{week:int}", (int year, int week, ILogSummaryService summaryService) => {
    var start = FirstDateOfWeek(year, week); 
    var end = start.AddDays(6); 

    var summary = summaryService.GetSummary(foodLog, start, end); 
    return Results.Json(summary); 
});

app.MapGet("/log/summary/{weeks:int?}", (int? weeks, ILogSummaryService summaryService) => {
    
    var (start, end) = SummaryUtils.GetDateRangeFromWeeks(weeks);

    var summary = summaryService.GetSummary(foodLog, start, end); 
    return Results.Json(summary); 
});


DateTime FirstDateOfWeek(int year, int weekOfYear)
{
    var jan1 = new DateTime(year, 1, 1); 
    int dayOffset = DayOfWeek.Monday - jan1.DayOfWeek;

    var firstMonday = jan1.AddDays(dayOffset); 
    return firstMonday.AddDays((weekOfYear - 1) * 7); 
}

app.Run();
