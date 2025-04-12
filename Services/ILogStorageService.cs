using FoodTrackerApi.Models;

namespace FoodTrackerApi.Services; 

public interface ILogStorageService
{
    Task<List<LogEntry>> LoadAllAsync(); 
    Task AddEntryAsync(LogEntry entry); 
    Task SaveAllAsync(List<LogEntry> entries);

    Task DeleteEntryAsync(int id); 
    Task ClearAllAsync(); 
}