namespace WebApp.Helpers;

//ChatGPT4o wrote this to easily calculate the project deadline.
public static class CalculateDeadline
{
    public static string GetDeadline(DateTime endDate)
    {
        var now = DateTime.Now;
        var daysLeft = (endDate.Date - now.Date).Days;
        if (daysLeft < 0) return "Overdue";
        if (daysLeft == 0) return "Due today";
        if (daysLeft == 1) return "1 day left";
        if (daysLeft < 7) return $"{daysLeft} days left";
        if (daysLeft < 14) return "1 week left";
        return $"{(int)Math.Ceiling(daysLeft / 7.0)} weeks left";
    }

}
