namespace FoodTrackerApi.Utils; 

public static class SummaryUtils 
{
    public static (DateTime Start, DateTime End) GetDateRangeFromWeeks(int? weeks)
    {
        var actualWeeks = weeks.GetValueOrDefault(1); 
        if (actualWeeks < 1 ) 
        {
            actualWeeks = 1; 
        }

        var start = DateTime.Today.AddDays(-7 * actualWeeks + 1); 
        var end = DateTime.Today;

        return (start, end);
    }
}