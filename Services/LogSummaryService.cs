namespace FoodTrackerApi.Services;

using System.Text.RegularExpressions;
using FoodTrackerApi.Models; 

public class LogSummaryService : ILogSummaryService
{
    public List<LogSummary> GetSummary(List<LogEntry> logEntries, DateTime start, DateTime end)
    {
        return logEntries
            .Where(e => e.Date.HasValue && e.Date.Value.Date >= start.Date && e.Date.Value.Date <= end.Date)
            .GroupBy(e => e.Date!.Value.Date)
            .Select(group => new LogSummary
            {
                Date = group.Key, 
                Good = group.Count(e => e.Status.ToLower() == "good"),
                Bad = group.Count(e => e.Status.ToLower() == "bad")
            })
            .OrderBy(s => s.Date)
            .ToList();
    }
}