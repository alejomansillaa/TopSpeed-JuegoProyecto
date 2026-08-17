using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicioManager : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [Tooltip("Nombre exacto de la escena de la pista en Build Settings")]
    public string nombreEscenaPista = "CarreraCuartoDeMilla";

    // 1/4 de milla = 402.33 metros
    public void SeleccionarCuartoDeMilla()
    {
        GuardarYIniciar("1/4 Milla", 402.33f);
    }

    // 1/2 milla = 804.67 metros
    public void SeleccionarMediaMilla()
    {
        GuardarYIniciar("1/2 Milla", 804.67f);
    }

    // 1 milla = 1609.34 metros
    public void SeleccionarUnaMilla()
    {
        GuardarYIniciar("1 Milla", 1609.34f);
    }

    private void GuardarYIniciar(string nombreModo, float distanciaMetros)
    {
        // Guardamos el nombre del modo y la distancia en metros para usarlo en la pista
        PlayerPrefs.SetString("ModoCarrera", nombreModo);
        PlayerPrefs.SetFloat("DistanciaMetros", distanciaMetros);
        PlayerPrefs.Save();

        // Cargamos la escena de la carrera
        SceneManager.LoadScene(nombreEscenaPista);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();

        // Permite probar la salida directamente desde el Editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}