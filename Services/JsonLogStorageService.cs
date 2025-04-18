using System.Text.Json;
using FoodTrackerApi.Models;

namespace FoodTrackerApi.Services; 

public class JsonLogStorageService : ILogStorageService
{
    private readonly string _filePath = "logs.json"; 
    private readonly JsonSerializerOptions _jsonOptions = new () { WriteIndented = true}; 

    public async Task<List<LogEntry>> LoadAllAsync()
    {
        if(!File.Exists(_filePath))
        { 
            return new List<LogEntry>(); 
        }

        var json = await File.ReadAllTextAsync(_filePath); 
        return JsonSerializer.Deserialize<List<LogEntry>>(json) ?? new List<LogEntry>();
    }

    public async Task AddEntryAsync(LogEntry entry) 
    {
         var logs = await LoadAllAsync(); 
         var nextId = logs.Any() ? logs.Max(e => e.Id) + 1 : 1; 
         entry.Id = nextId;
         logs.Add(entry); 
         await SaveAllAsync(logs);
    }

    public async Task SaveAllAsync(List<LogEntry> entries)
    {
        var json = JsonSerializer.Serialize(entries, _jsonOptions); 
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task DeleteEntryAsync(int id)
    {
        var logs = await LoadAllAsync(); 
        var updated = logs.Where(e => e.Id != id).ToList();
        await SaveAllAsync(updated);
    }

    public async Task ClearAllAsync() 
    {
        await SaveAllAsync(new List<LogEntry>()); 
    }
}