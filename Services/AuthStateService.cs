using Blazored.LocalStorage; // Aseg�rate de tener este 'using' si usas Blazored.LocalStorage

namespace EnFocoFRONT.Services
{
    public class AuthStateService : IAuthStateService
    {
        public event Action? OnAuthStateChanged;

        // La propiedad IsAuthenticated ahora solo tendr� un 'get' p�blico
        // y un 'set' privado, para que el estado se controle internamente
        // y el evento se dispare siempre que cambie.
        private bool _isAuthenticated = false; // Valor predeterminado inicial

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            // El 'set' ya no es p�blico directamente; se gestiona por el m�todo SetAuthenticationState.
            // Si tu interfaz exige 'public set', el patr�n anterior es el que tienes.
            // Pero para un control m�s robusto y disparo del evento, es mejor as�.
            // Para adherirse a tu interfaz (public set), usaremos el patr�n original pero con inicializaci�n.
            // Dejamos el 'set' en el c�digo para el 'get' de la interfaz.
            set // �Cuidado! si cambias IsAuthenticated directamente desde fuera, tambi�n disparar� el evento.
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
            // Al construir el servicio, intentamos cargar el estado de autenticaci�n inicial
            // Esto es crucial para que el estado se refleje correctamente al cargar la aplicaci�n.
            InitializeAuthenticationStateAsync();
        }

        // Este m�todo se llamar� autom�ticamente al construir el servicio
        // y comprobar� el token en el localStorage.
        private async void InitializeAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorageService.GetItemAsync<string>("authToken");
                // Establece IsAuthenticated, lo que autom�ticamente disparar� NotifyStateChanged()
                // si el estado ha cambiado (por ejemplo, de false a true si hab�a un token).
                IsAuthenticated = !string.IsNullOrEmpty(token);
                Console.WriteLine($"AuthStateService: Estado inicial cargado. IsAuthenticated = {IsAuthenticated}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar AuthStateService: {ex.Message}");
                IsAuthenticated = false; // Asegurarse de que no est� autenticado si hay un error
            }
        }

        // M�todo para disparar el evento (ya lo ten�as)
        public void NotifyStateChanged()
        {
            Console.WriteLine("AuthStateService: Notificando cambio de estado.");
            OnAuthStateChanged?.Invoke();
        }
    }
}