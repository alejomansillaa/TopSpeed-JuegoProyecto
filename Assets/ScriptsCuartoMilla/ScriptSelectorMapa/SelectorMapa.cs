using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorMapa : MonoBehaviour
{
    [Header("Escenas")]
    public string escenaCarrera = "CarreraCuartoDeMilla";
    public string escenaMenuInicio = "MenuDelInicio";

    public void SeleccionarDia() => GuardarMapaYJugar("Dia");
    public void SeleccionarTarde() => GuardarMapaYJugar("Tarde");
    public void SeleccionarNoche() => GuardarMapaYJugar("Noche");

    private void GuardarMapaYJugar(string mapaElegido)
    {
        PlayerPrefs.SetString("MapaTiempo", mapaElegido);
        PlayerPrefs.Save();

        SceneManager.LoadScene(escenaCarrera);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene(escenaMenuInicio);
    }
}