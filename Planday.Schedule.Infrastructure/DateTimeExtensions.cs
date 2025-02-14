namespace Planday.Schedule.Infrastructure;

public static class DateTimeExtensions
{
    public static string SqlDateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
    
    public static string ToInvariantFormat(this DateTime dateTime, string format) => 
        dateTime.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
}