public interface IAppSettingsService
{
    AppSettings? Settings { get; }
    Task LoadAsync();
}
