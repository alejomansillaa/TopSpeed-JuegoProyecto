using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicioManager : MonoBehaviour
{
    [Header("Siguiente Escena")]
    [Tooltip("Nombre exacto de la escena para elegir el mapa")]
    public string MenuSelectorMapa = "SeleccionMapa";

    public void SeleccionarCuartoDeMilla() => GuardarYAvanzar("1/4 Milla", 402.33f);
    public void SeleccionarMediaMilla() => GuardarYAvanzar("1/2 Milla", 804.67f);
    public void SeleccionarUnaMilla() => GuardarYAvanzar("1 Milla", 1609.34f);

    private void GuardarYAvanzar(string nombreModo, float distanciaMetros)
    {
        PlayerPrefs.SetString("ModoCarrera", nombreModo);
        PlayerPrefs.SetFloat("DistanciaMetros", distanciaMetros);
        PlayerPrefs.Save();

        // Carga la nueva escena de selección de mapa
        SceneManager.LoadScene(MenuSelectorMapa);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}