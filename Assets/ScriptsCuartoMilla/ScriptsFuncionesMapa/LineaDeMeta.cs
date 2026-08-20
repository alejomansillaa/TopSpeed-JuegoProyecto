using UnityEngine;
using UnityEngine.SceneManagement;

public class LineaDeMeta : MonoBehaviour
{
    [Header("Referencia al Auto")]
    public Transform auto;

    [Header("UI de Victoria")]
    public GameObject panelGanaste;

    [Header("Estadísticas / Cronómetro")]
    public MonoBehaviour scriptEstadisticas;

    private bool carreraFinalizada = false;

    void Start()
    {
        Time.timeScale = 1f;
        carreraFinalizada = false;

        if (panelGanaste != null)
            panelGanaste.SetActive(false);

        if (MusicaFondo.Instance != null)
            MusicaFondo.Instance.Reproducir();

        AcomodarMeta();
    }

    // Acomoda la meta en X según la distancia, conservando la altura y tamaño del editor
    public void AcomodarMeta()
    {
        if (auto == null) return;

        float distanciaMetros = PlayerPrefs.GetFloat("DistanciaMetros", 402.33f);
        float posXFinal = auto.position.x + distanciaMetros;

        // Se mantiene la coordenada Y y la escala que acomodaste manualmente en la escena
        transform.position = new Vector3(posXFinal, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (carreraFinalizada) return;

        bool esElAuto = collision.CompareTag("Player") ||
                        (auto != null && (collision.transform == auto || collision.transform.IsChildOf(auto)));

        if (esElAuto)
        {
            carreraFinalizada = true;

            // 1. Activar el Menú de Victoria
            if (panelGanaste != null)
                panelGanaste.SetActive(true);

            // 2. Detener Cronómetro / Estadísticas
            if (scriptEstadisticas != null)
                scriptEstadisticas.enabled = false;

            // 3. Detener la música
            if (MusicaFondo.Instance != null)
                MusicaFondo.Instance.Detener();

            // 4. Detener la física del auto por completo
            Rigidbody2D rb = collision.attachedRigidbody;
            if (rb == null && auto != null) rb = auto.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.simulated = false; // Apaga la física
            }

            // 5. Congelar el tiempo del juego
            Time.timeScale = 0f;
        }
    }

    public void ReiniciarCarrera()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}