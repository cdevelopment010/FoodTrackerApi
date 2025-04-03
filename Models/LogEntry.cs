namespace FoodTrackerApi.Models;

public class LogEntry 
{
    public DateTime? Date { get; set; }
    public string Status { get; set; } = "good";

    public int UserId { get; set; } = -1;
    public string? Meal {get; set; }
    public string? Notes { get; set; }
}