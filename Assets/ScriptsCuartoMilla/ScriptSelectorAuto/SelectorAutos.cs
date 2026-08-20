using UnityEngine;

public class SelectorAutos : MonoBehaviour
{
    [Header("Lista de Autos")]
    public GameObject[] autos;

    private int indiceActual = 0;

    private void Start()
    {
        MostrarAutoActual();
    }

    public void SiguienteAuto()
    {
        Debug.Log("Botón Siguiente presionado");
        indiceActual++;
        if (indiceActual >= autos.Length)
        {
            indiceActual = 0;
        }
        MostrarAutoActual();
    }

    public void AnteriorAuto()
    {
        Debug.Log("Botón Anterior presionado");
        indiceActual--;
        if (indiceActual < 0)
        {
            indiceActual = autos.Length - 1;
        }
        MostrarAutoActual();
    }

    private void MostrarAutoActual()
    {
        for (int i = 0; i < autos.Length; i++)
        {
            if (autos[i] != null)
            {
                autos[i].SetActive(i == indiceActual);
            }
        }

        // Guarda el auto elegido para la carrera
        PlayerPrefs.SetInt("AutoSeleccionado", indiceActual);
    }

    public void Jugar()
    {
        // Carga la escena principal de la carrera
        UnityEngine.SceneManagement.SceneManager.LoadScene("CarreraDrag");
    }
}