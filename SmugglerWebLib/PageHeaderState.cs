public class PageHeaderState
{
    public string Title { get; private set; } = string.Empty;
    public string LatestDateTime { get; private set; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
    public string PublishDateTime { get; private set; } = string.Empty;
    public bool ShowPublishDateTime { get; private set; }

    public event Action? OnChange;

    public void Set(string title, DateTime? publishDateTime = null, DateTime? latestDateTime = null)
    {
        Title = title;
        ShowPublishDateTime = publishDateTime is not null;
        PublishDateTime = publishDateTime.CheckNullToDateTimeString();
        LatestDateTime = latestDateTime.ToDateTimeString() ?? DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        OnChange?.Invoke();
    }
}

public static class StateFormatExtensions
{
    public static string CheckNullToDateTimeString(this DateTime? dateTime)
    {
        return dateTime?.ToString("yyyy/MM/dd HH:mm") ?? string.Empty;
    }

    public static string ToDateTimeString(this DateTime? dateTime)
    {
        return dateTime?.ToString("yyyy/MM/dd HH:mm");
    }
}