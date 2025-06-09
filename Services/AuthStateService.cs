
namespace EnFocoFRONT.Services
{
    public class AuthStateService : IAuthStateService
    {
        public event Action? OnAuthStateChanged;

        private bool _isAuthenticated = false;

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                _isAuthenticated = value;
                NotifyStateChanged();
            }
        }

        public void NotifyStateChanged() => OnAuthStateChanged?.Invoke();
    }
}