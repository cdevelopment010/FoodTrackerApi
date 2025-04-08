using System.ComponentModel.DataAnnotations;

namespace FoodTrackerApi.Models;

public class LogEntry 
{
    public DateTime? Date { get; set; }
    [EnumDataType(typeof(FoodStatus))]
    public FoodStatus Status { get; set; } = FoodStatus.Good;

    public int UserId { get; set; } = -1;
    public string? Meal {get; set; }
    public string? Notes { get; set; }
}

public enum FoodStatus
{
    Good  = 0, 
    Bad  = 1, 
    Neutral = 2,
    Hypo = 3,
}   