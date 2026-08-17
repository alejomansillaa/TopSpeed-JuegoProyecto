using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    [Tooltip("Arrastra aquí el objeto de tu AutoJugador.")]
    public Transform objetivo;

    [Header("Ajustes de Cámara")]
    [Tooltip("Suavizado del movimiento. Valores entre 3 y 8 suelen verse muy bien.")]
    public float velocidadSuavizado = 5f;

    [Tooltip("Desplazamiento respecto al auto (El valor Z SIEMPRE debe ser -10 en 2D).")]
    public Vector3 offset = new Vector3(3f, 0f, -10f);

    // LateUpdate se ejecuta después de que el auto ya se movió en Update()
    void LateUpdate()
    {
        // Si no hemos asignado un auto, no hace nada para evitar errores
        if (objetivo == null) return;

        // Calculamos la posición a la que la cámara debería ir
        Vector3 posicionDeseada = objetivo.position + offset;

        // Interpolamos de forma suave desde la posición actual hacia la deseada
        Vector3 posicionSuave = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado * Time.deltaTime);

        // Aplicamos la nueva posición a la cámara
        transform.position = posicionSuave;
    }

}