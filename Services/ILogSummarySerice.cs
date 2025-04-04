namespace FoodTrackerApi.Services; 

using FoodTrackerApi.Models; 

public interface ILogSummaryService
{
    List<LogSummary> GetSummary(List<LogEntry> logEntries, DateTime start, DateTime end); 
}