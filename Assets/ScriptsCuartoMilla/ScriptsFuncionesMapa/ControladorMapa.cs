using UnityEngine;

public class ControladorMapa : MonoBehaviour
{
    [Header("Objetos de Fondo")]
    public GameObject fondoDia;
    public GameObject fondoAtardecer;
    public GameObject fondoNoche;

    void Start()
    {
        string mapaElegido = PlayerPrefs.GetString("MapaTiempo", "Dia");

        // Apagar todos los fondos
        if (fondoDia != null) fondoDia.SetActive(false);
        if (fondoAtardecer != null) fondoAtardecer.SetActive(false);
        if (fondoNoche != null) fondoNoche.SetActive(false);

        // Activar el fondo correspondiente
        switch (mapaElegido)
        {
            case "Atardecer":
            case "Tarde":
                if (fondoAtardecer != null) fondoAtardecer.SetActive(true);
                break;

            case "Noche":
                if (fondoNoche != null) fondoNoche.SetActive(true);
                break;

            case "Dia":
            default:
                if (fondoDia != null) fondoDia.SetActive(true);
                break;
        }
    }
}