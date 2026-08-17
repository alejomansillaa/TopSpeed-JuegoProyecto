using UnityEngine;

public class RotadorRueda : MonoBehaviour
{
    [Header("Ajuste Visual")]
    [Tooltip("Sensibilidad de giro de la rueda.")]
    public float multiplicadorGiro = 100f;

    private ControladorAuto controladorAuto;

    void Start()
    {
        // Obtiene el script ControladorAuto del objeto padre (AutoJugador)
        controladorAuto = GetComponentInParent<ControladorAuto>();
    }

    void Update()
    {
        if (controladorAuto == null) return;

        // Leemos la velocidad a la que va el auto
        float velocidadActual = controladorAuto.GetVelocidadActual();

        // En 2D, rotar en Z negativo (-Z) hace que gire en sentido horario (hacia adelante)
        float gradosARotar = -velocidadActual * multiplicadorGiro * Time.deltaTime;

        transform.Rotate(0f, 0f, gradosARotar);
    }
}