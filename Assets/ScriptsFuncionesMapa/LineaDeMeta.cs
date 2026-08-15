using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class LineaDeMeta : MonoBehaviour
{
    private float tiempoInicio;
    private bool carreraTerminada = false;

    [Header("UI de Victoria")]
    public GameObject panelVictoria; // Arrastra el PanelVictoria aquí

    void Start()
    {
        tiempoInicio = Time.time;

        // Nos aseguramos de que el panel de victoria comience oculto
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !carreraTerminada)
        {
            carreraTerminada = true;

            float tiempoFinal = Time.time - tiempoInicio;
            Debug.Log($"¡LLEGASTE A LA META! 🏁 Tiempo oficial: {tiempoFinal:F2} segundos.");

            // 1. Mostrar el menú de victoria
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);
            }

            // 2. Pausar el juego por completo
            Time.timeScale = 0f;
        }
    }

    // 🔄 Función para el Botón REINICAR
    public void ReiniciarCarrera()
    {
        Time.timeScale = 1f; // ¡CRUCIAL! Reanuda el tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 🚪 Función para el Botón SALIR
    public void SalirDelJuego()
    {
        Time.timeScale = 1f;
        Debug.Log("Saliendo del juego...");

        Application.Quit(); // Funciona en la compilación ejecutable (.exe / apk)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el modo Play dentro de Unity Editor
#endif
    }
}