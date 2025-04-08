namespace FoodTrackerApi.Models; 

public class LogSummary 
{
    public DateTime Date { get; set; }
    public int Good { get; set; }
    public int Bad { get; set; }
    public int Hypo { get; set; } = 0;
    public int Neutral { get; set; } = 0;
}