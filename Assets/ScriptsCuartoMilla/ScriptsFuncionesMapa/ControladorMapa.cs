using UnityEngine;

public class ControladorMapa : MonoBehaviour
{
    [Header("Objetos de Fondo")]
    public GameObject fondoDia;
    public GameObject fondoTarde;
    public GameObject fondoNoche;

    [Header("Referencia a la Meta")]
    public Transform lineaDeMeta;

    [Header("Ajustes de Meta - DÍA")]
    public float posYDia = 0f;
    public Vector3 escalaDia = new Vector3(1f, 1f, 1f);

    [Header("Ajustes de Meta - TARDE")]
    public float posYTarde = 0f;
    public Vector3 escalaTarde = new Vector3(1f, 1f, 1f);

    [Header("Ajustes de Meta - NOCHE")]
    public float posYNoche = 0f;
    public Vector3 escalaNoche = new Vector3(1f, 1f, 1f);

    void Start()
    {
        string mapaElegido = PlayerPrefs.GetString("MapaTiempo", "Dia");

        // Ocultar todos los fondos
        if (fondoDia != null) fondoDia.SetActive(false);
        if (fondoTarde != null) fondoTarde.SetActive(false);
        if (fondoNoche != null) fondoNoche.SetActive(false);

        // Activar el elegido y adaptar la meta
        switch (mapaElegido)
        {
            case "Dia":
                if (fondoDia != null) fondoDia.SetActive(true);
                AjustarMeta(posYDia, escalaDia);
                break;

            case "Tarde":
                if (fondoTarde != null) fondoTarde.SetActive(true);
                AjustarMeta(posYTarde, escalaTarde);
                break;

            case "Noche":
                if (fondoNoche != null) fondoNoche.SetActive(true);
                AjustarMeta(posYNoche, escalaNoche);
                break;
        }
    }

    void AjustarMeta(float posY, Vector3 escala)
    {
        if (lineaDeMeta != null)
        {
            // Mantiene la distancia horizontal (X) pero cambia la altura (Y)
            Vector3 pos = lineaDeMeta.position;
            pos.y = posY;
            lineaDeMeta.position = pos;

            // Cambia el tamaño/escala de la meta
            lineaDeMeta.localScale = escala;
        }
    }
}