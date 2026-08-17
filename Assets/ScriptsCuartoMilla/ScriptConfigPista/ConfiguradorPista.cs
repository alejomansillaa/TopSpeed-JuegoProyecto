using UnityEngine;

public class ConfiguradorPista : MonoBehaviour
{
    public Transform lineaDeMeta;
    public Transform lineaDeSalida;

    void Start()
    {
        // Recuperamos la distancia guardada (por defecto 402.33m)
        float distanciaMetros = PlayerPrefs.GetFloat("DistanciaMetros", 402.33f);

        if (lineaDeMeta != null && lineaDeSalida != null)
        {
            Vector3 posicionMeta = lineaDeMeta.position;
            // Desplazamos la meta sobre el eje X según la salida
            posicionMeta.x = lineaDeSalida.position.x + distanciaMetros;
            lineaDeMeta.position = posicionMeta;
        }
    }
}