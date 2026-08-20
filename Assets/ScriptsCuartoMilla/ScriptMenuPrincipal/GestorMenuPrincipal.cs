using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorMenuPrincipal : MonoBehaviour
{
    [Header("Configuración de Escenas")]
    [Tooltip("Nombre exacto de la escena del selector de modos")]
    public string nombreMenuSelectorModo = "MenuSelectorModo";

    // Llamado por el botón Jugar
    public void Jugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreMenuSelectorModo);
    }

    // Llamado por el botón Garage (Pendiente de desarrollo)
    public void AbrirGarage()
    {
        Debug.Log("La escena del Garage aún no está desarrollada.");
    }

    // Llamado por el botón Modo Libre (Pendiente de desarrollo)
    public void AbrirModoLibre()
    {
        Debug.Log("El Modo Libre aún no está desarrollado.");
    }

    // Llamado por el botón Torneo (Pendiente de desarrollo)
    public void AbrirTorneo()
    {
        Debug.Log("El modo Torneo aún no está desarrollado.");
    }

    // Llamado por el botón Salir
    public void SalirDelJuego()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}