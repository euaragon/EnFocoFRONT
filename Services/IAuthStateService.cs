public interface IAuthStateService
{
    event Action? OnAuthStateChanged;
    bool IsAuthenticated { get; set; }
    void NotifyStateChanged();
}
