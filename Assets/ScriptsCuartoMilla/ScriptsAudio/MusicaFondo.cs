using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicaFondo : MonoBehaviour
{
    public static MusicaFondo Instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Mantiene la música sonando sin reiniciarse si cambias de escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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