namespace Shared.Extensions;

public static class DateTimeOffsetExtension
{
    public static bool IsAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset compareTo)
    {
        return dateTimeOffset > compareTo;
    }

    public static bool IsBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset compareTo)
    {
        return dateTimeOffset < compareTo;
    }

    public static int GetQuarter(this DateTimeOffset dateTimeOffset)
    {
        return (dateTimeOffset.Month + 2) / 3;
    }
}