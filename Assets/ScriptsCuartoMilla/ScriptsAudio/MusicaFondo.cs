using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicaFondo : MonoBehaviour
{
    public static MusicaFondo Instance;

    [Header("Lista de Canciones")]
    public AudioClip[] playlist;

    private AudioSource audioSource;

    void Awake()
    {
        // Mantiene la música sonando sin reiniciarse si cambias de escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        // Si no está sonando nada al iniciar, arranca una canción
        if (!audioSource.isPlaying)
        {
            ReproducirCancionAleatoria();
        }
    }

    // Método público llamado desde LineaDeMeta.cs al reiniciar la carrera
    public void Reproducir()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        if (!audioSource.isPlaying)
        {
            ReproducirCancionAleatoria();
        }
    }

    public void ReproducirCancionAleatoria()
    {
        if (playlist == null || playlist.Length == 0) return;
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        // Recupera la última canción jugada para no repetirla consecutivamente
        int ultimaCancion = PlayerPrefs.GetInt("UltimaCancionIndex", -1);
        int nuevoIndex = Random.Range(0, playlist.Length);

        if (playlist.Length > 1)
        {
            while (nuevoIndex == ultimaCancion)
            {
                nuevoIndex = Random.Range(0, playlist.Length);
            }
        }

        // Guarda el índice actual para la próxima ejecución
        PlayerPrefs.SetInt("UltimaCancionIndex", nuevoIndex);
        PlayerPrefs.Save();

        // Asigna la canción seleccionada y la reproduce
        audioSource.clip = playlist[nuevoIndex];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void CambiarVolumen(float volumen)
    {
        if (audioSource != null)
            audioSource.volume = Mathf.Clamp01(volumen);
    }

    public void Pausar() => audioSource?.Pause();
    public void Reanudar() => audioSource?.UnPause();
    public void Detener() => audioSource?.Stop();
}