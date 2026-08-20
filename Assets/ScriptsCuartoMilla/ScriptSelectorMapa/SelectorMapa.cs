using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorMapa : MonoBehaviour
{
    [Header("Escenas de Navegación")]
    [Tooltip("Escena a la que irá tras seleccionar un mapa")]
    public string escenaSiguiente = "MenuSeleccionAuto";

    [Tooltip("Escena a la que irá al presionar el botón Volver")]
    public string escenaMenuInicio = "MenuSelectorModo";

    public void SeleccionarMapaDia()
    {
        PlayerPrefs.SetString("MapaSeleccionado", "Dia");
        CargarSiguiente();
    }

    public void SeleccionarMapaTarde()
    {
        PlayerPrefs.SetString("MapaSeleccionado", "Atardecer");
        CargarSiguiente();
    }

    public void SeleccionarMapaNoche()
    {
        PlayerPrefs.SetString("MapaSeleccionado", "Noche");
        CargarSiguiente();
    }

    private void CargarSiguiente()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaSiguiente);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaMenuInicio);
    }
}