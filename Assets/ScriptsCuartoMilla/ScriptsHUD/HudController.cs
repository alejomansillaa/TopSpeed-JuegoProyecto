using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class HudController : MonoBehaviour
{
    [Header("Referencia al Auto")]
    // Si tu script de auto se llama distinto (ej. MovilidadAuto), cambia 'ControladorAuto' por ese nombre
    public ControladorAuto auto;

    [Header("Componentes de Texto (TextMeshPro)")]
    public TextMeshProUGUI textoVelocidad;
    public TextMeshProUGUI textoRPM;
    public TextMeshProUGUI textoMarcha;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoModo;

    [Header("Estado del Cronómetro")]
    private float tiempoCarrera = 0f;
    private bool carreraActiva = true;

    void Start()
    {
        // Buscar el auto automáticamente en la escena si no se asignó en el Inspector
        if (auto == null)
            auto = FindAnyObjectByType<ControladorAuto>();

        // Mostrar el nombre del modo guardado desde el menú
        if (textoModo != null)
        {
            string modoElegido = PlayerPrefs.GetString("ModoCarrera", "1/4 Milla");
            textoModo.text = "Modo: " + modoElegido;
        }
    }

    void Update()
    {
        ActualizarDatosAuto();

        if (carreraActiva)
        {
            tiempoCarrera += Time.deltaTime;
            ActualizarCronometro();
        }
    }

    void ActualizarDatosAuto()
    {
        if (auto == null) return;

        // Actualiza el texto de Velocidad
        if (textoVelocidad != null)
            textoVelocidad.text = Mathf.RoundToInt(auto.velocidadKmh).ToString() + " KM/H";

        // Actualiza el texto de RPM
        if (textoRPM != null)
            textoRPM.text = Mathf.RoundToInt(auto.rpmActual).ToString() + " RPM";

        // Actualiza la Marcha actual
        if (textoMarcha != null)
        {
            string marchaTexto = auto.marchaActual == 0 ? "N" : auto.marchaActual.ToString();
            textoMarcha.text = "Marcha: " + marchaTexto;
        }
    }

    void ActualizarCronometro()
    {
        if (textoTiempo != null)
        {
            // Formato de tiempo con dos decimales (ejemplo: 08.45 s)
            textoTiempo.text = tiempoCarrera.ToString("F2") + " s";
        }
    }

    // Llamar a este método cuando el auto cruce la meta
    public void DetenerCronometro()
    {
        carreraActiva = false;
    }
}