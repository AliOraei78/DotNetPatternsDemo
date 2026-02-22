// Ambient Context
public static class UserContext
{
    private static readonly AsyncLocal<string> _currentUserId = new();

    public static string CurrentUserId
    {
        get => _currentUserId.Value ?? "Anonymous";
        set => _currentUserId.Value = value;
    }
}