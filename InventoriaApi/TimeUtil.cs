namespace InventoriaApi;

public static class TimeUtil
{
    public static DateTime GetDanishDatetime()
    {
        TimeZoneInfo danishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        DateTime utcNow = DateTime.UtcNow;
        return TimeZoneInfo.ConvertTimeFromUtc(utcNow, danishTimeZone);
    }
}
