using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicio : MonoBehaviour
{
    [Header("Navegación")]
    [Tooltip("Escribe el nombre exacto de la escena del menú principal")]
    public string nombreMenuPrincipal = "MenuPrincipal";

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreMenuPrincipal);
    }
}