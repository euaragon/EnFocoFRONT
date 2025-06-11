using Blazored.LocalStorage; // Asegúrate de tener este 'using' si usas Blazored.LocalStorage

namespace EnFocoFRONT.Services
{
    public class AuthStateService : IAuthStateService
    {
        public event Action? OnAuthStateChanged;

        // La propiedad IsAuthenticated ahora solo tendrá un 'get' público
        // y un 'set' privado, para que el estado se controle internamente
        // y el evento se dispare siempre que cambie.
        private bool _isAuthenticated = false; // Valor predeterminado inicial

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            // El 'set' ya no es público directamente; se gestiona por el método SetAuthenticationState.
            // Si tu interfaz exige 'public set', el patrón anterior es el que tienes.
            // Pero para un control más robusto y disparo del evento, es mejor así.
            // Para adherirse a tu interfaz (public set), usaremos el patrón original pero con inicialización.
            // Dejamos el 'set' en el código para el 'get' de la interfaz.
            set // ¡Cuidado! si cambias IsAuthenticated directamente desde fuera, también disparará el evento.
            {
                // Solo notifica si el valor cambia para evitar re-renderizaciones innecesarias
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    NotifyStateChanged();
                }
            }
        }

        // Inyectar ILocalStorageService en el constructor
        private readonly ILocalStorageService _localStorageService;

        public AuthStateService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            // Al construir el servicio, intentamos cargar el estado de autenticación inicial
            // Esto es crucial para que el estado se refleje correctamente al cargar la aplicación.
            InitializeAuthenticationStateAsync();
        }

        // Este método se llamará automáticamente al construir el servicio
        // y comprobará el token en el localStorage.
        private async void InitializeAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorageService.GetItemAsync<string>("authToken");
                // Establece IsAuthenticated, lo que automáticamente disparará NotifyStateChanged()
                // si el estado ha cambiado (por ejemplo, de false a true si había un token).
                IsAuthenticated = !string.IsNullOrEmpty(token);
                Console.WriteLine($"AuthStateService: Estado inicial cargado. IsAuthenticated = {IsAuthenticated}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar AuthStateService: {ex.Message}");
                IsAuthenticated = false; // Asegurarse de que no esté autenticado si hay un error
            }
        }

        // Método para disparar el evento (ya lo tenías)
        public void NotifyStateChanged()
        {
            Console.WriteLine("AuthStateService: Notificando cambio de estado.");
            OnAuthStateChanged?.Invoke();
        }
    }
}