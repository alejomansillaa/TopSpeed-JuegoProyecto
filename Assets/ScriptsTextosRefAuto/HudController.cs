using UnityEngine;
using TMPro;

public class HudCarrera : MonoBehaviour
{
    [Header("Referencia al Auto")]
    public ControladorAuto auto;

    [Header("Textos de la Interfaz")]
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoVelocidad;
    public TextMeshProUGUI textoRPM;
    public TextMeshProUGUI textoMarcha;

    private float tiempoCarrera = 0f;

    void Update()
    {
        if (Time.timeScale > 0f)
        {
            tiempoCarrera += Time.deltaTime;
            ActualizarTiempoUI();
        }

        if (auto != null)
        {
            ActualizarAutoUI(); // <--- Aquí se llama a la función
        }
    }

    void ActualizarTiempoUI()
    {
        if (textoTiempo != null)
        {
            textoTiempo.text = $"TIEMPO: {tiempoCarrera:F2}s";
        }
    }

    // ⬇️ AQUÍ VA EL CÓDIGO DE TU IMAGEN ⬇️
    void ActualizarAutoUI()
    {
        if (textoVelocidad != null)
        {
            textoVelocidad.text = $"{Mathf.RoundToInt(auto.velocidadKmh)} KM/H";
        }

        if (textoRPM != null)
        {
            textoRPM.text = $"{Mathf.RoundToInt(auto.rpmActual)} RPM";
        }

        if (textoMarcha != null)
        {
            // Muestra "N" si está en 0, de lo contrario muestra el número de marcha
            string textoM = auto.marchaActual == 0 ? "N" : auto.marchaActual.ToString();
            textoMarcha.text = $"MARCHA: {textoM}";
        }
    }
}