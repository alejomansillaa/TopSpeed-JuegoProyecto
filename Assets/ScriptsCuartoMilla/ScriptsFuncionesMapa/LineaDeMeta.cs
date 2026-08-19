using UnityEngine;
using UnityEngine.SceneManagement; // Requerido para reiniciar la escena

public class LineaDeMeta : MonoBehaviour
{
    [Header("Referencia al Auto")]
    public Transform auto;

    [Header("UI de Victoria")]
    [Tooltip("Arrastra aquí tu panel de ¡Ganaste! (que contiene los botones Reiniciar y Salir)")]
    public GameObject panelGanaste;

    [Header("Estadísticas / Cronómetro (Opcional)")]
    [Tooltip("Arrastra aquí el script de estadísticas o cronómetro para pausarlo al ganar")]
    public MonoBehaviour scriptEstadisticas;

    [Header("Ajustes de Meta - DÍA")]
    public float posYDia = 0f;
    public Vector3 escalaDia = Vector3.one;

    [Header("Ajustes de Meta - TARDE")]
    public float posYTarde = 0f;
    public Vector3 escalaTarde = Vector3.one;

    [Header("Ajustes de Meta - NOCHE")]
    public float posYNoche = 0f;
    public Vector3 escalaNoche = Vector3.one;

    private bool carreraFinalizada = false;

    void Start()
    {
        // 1. Restaurar el tiempo normal del juego
        Time.timeScale = 1f;
        carreraFinalizada = false;

        // 2. Ocultar el panel de victoria al empezar/reiniciar
        if (panelGanaste != null)
        {
            panelGanaste.SetActive(false);
        }

        // 3. Reanudar o iniciar la música de fondo
        if (MusicaFondo.Instance != null)
        {
            MusicaFondo.Instance.Reproducir();
        }

        // 4. Posicionar la meta según la distancia y el mapa
        AcomodarMeta();
    }

    private void AcomodarMeta()
    {
        if (auto == null) return;

        float distanciaMetros = PlayerPrefs.GetFloat("DistanciaMetros", 402.33f);
        float posXFinal = auto.position.x + distanciaMetros;

        string mapaElegido = PlayerPrefs.GetString("MapaTiempo", "Dia");
        float posYFinal = posYDia;
        Vector3 escalaFinal = escalaDia;

        switch (mapaElegido)
        {
            case "Tarde":
                posYFinal = posYTarde;
                escalaFinal = escalaTarde;
                break;

            case "Noche":
                posYFinal = posYNoche;
                escalaFinal = escalaNoche;
                break;

            case "Dia":
            default:
                posYFinal = posYDia;
                escalaFinal = escalaDia;
                break;
        }

        transform.position = new Vector3(posXFinal, posYFinal, transform.position.z);
        transform.localScale = escalaFinal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (carreraFinalizada) return;

        bool esElAuto = collision.CompareTag("Player") ||
                        (auto != null && (collision.transform == auto || collision.transform.IsChildOf(auto)));

        if (esElAuto)
        {
            carreraFinalizada = true;

            // 1. Activar el Menú de Victoria (!Ganaste!)
            if (panelGanaste != null)
            {
                panelGanaste.SetActive(true);
            }

            // 2. Detener las Estadísticas / Cronómetro
            if (scriptEstadisticas != null)
            {
                scriptEstadisticas.enabled = false;
            }

            // 3. Detener la música de fondo
            if (MusicaFondo.Instance != null)
            {
                MusicaFondo.Instance.Detener();
            }

            // 4. Detener el movimiento físico del vehículo
            if (auto != null)
            {
                Rigidbody2D rb = auto.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }

            // 5. Congelar el tiempo del juego
            Time.timeScale = 0f;
        }
    }

    // ========================================================
    // MÉTODOS PÚBLICOS PARA LOS BOTONES DEL MENÚ DE VICTORIA
    // ========================================================

    // Botón "Reiniciar"
    public void ReiniciarCarrera()
    {
        Time.timeScale = 1f; // Descongela el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual de cero
    }

    // Botón "Salir" (Cierra la aplicación por completo)
    public void SalirAlMenu()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para la ejecución si estás probando dentro de Unity
#else
        Application.Quit(); // Cierra el ejecutable del juego (.exe, APK, etc.)
#endif
    }
}