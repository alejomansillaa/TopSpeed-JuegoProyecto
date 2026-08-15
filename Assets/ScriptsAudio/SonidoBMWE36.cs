using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMotor : MonoBehaviour
{
    [Header("Referencia al Auto")]
    public ControladorAuto auto;

    [Header("Ajustes de Pitch (Tono)")]
    public float pitchMinimo = 0.8f;
    public float pitchMaximo = 2.2f;

    [Header("Ajustes de Volumen")]
    public float volumenMaximo = 0.8f;

    [Header("Suavizado de Audio")]
    public float velocidadSuavizado = 5f;

    private AudioSource audioSource;
    private float pitchObjetivo;
    private float volumenObjetivo;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (auto == null)
        {
            auto = GetComponentInParent<ControladorAuto>();
            if (auto == null) auto = FindAnyObjectByType<ControladorAuto>();
        }
    }

    void Update()
    {
        if (auto == null) return;

        // Detectamos si presiona 'W' / Flecha Arriba O si el auto se está moviendo por inercia
        bool estaAcelerandoOMoviendose = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || auto.velocidadKmh > 1f;

        if (estaAcelerandoOMoviendose)
        {
            // Inicia la reproducción si estaba apagado
            if (!audioSource.isPlaying) audioSource.Play();

            float progresoRPM = Mathf.InverseLerp(auto.rpmMinimas, auto.rpmMaximas, auto.rpmActual);

            pitchObjetivo = Mathf.Lerp(pitchMinimo, pitchMaximo, progresoRPM);
            volumenObjetivo = Mathf.Lerp(0.3f, volumenMaximo, progresoRPM);
        }
        else
        {
            // Si soltaste la tecla y el auto se detuvo, desvanece el volumen hasta apagar
            volumenObjetivo = 0f;
            if (audioSource.volume <= 0.02f && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Aplicamos cambios suaves para evitar chasquidos repentinos de audio
        audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, pitchObjetivo, velocidadSuavizado * Time.deltaTime);
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, volumenObjetivo, velocidadSuavizado * Time.deltaTime);
    }
}