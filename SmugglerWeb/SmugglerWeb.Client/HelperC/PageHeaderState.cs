public class PageHeaderState
{
    public string Title { get; private set; } = string.Empty;
    public string LatestDateTime { get; private set; } = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

    public event Action? OnChange;

    public void Set(string title, string? latestDateTime = null)
    {
        Title = title;
        LatestDateTime = latestDateTime ?? DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        OnChange?.Invoke();
    }
}