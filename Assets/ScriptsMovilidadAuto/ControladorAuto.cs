using UnityEngine;

public class ControladorAuto : MonoBehaviour
{
    [Header("Configuración del Motor")]
    public float fuerzaMotor = 1200f;
    public float desaceleracion = 60f;     // Fuerza con la que frena al soltar la 'W'
    public float rpmMinimas = 0f;       // Ralentí
    public float rpmMaximas = 9000f;       // Corte de inyección
    public float velocidadAgujaRPM = 5000f; // Rapidez con la que se mueve el indicador de RPM

    [Header("Telemetría")]
    public float velocidadKmh;
    public float rpmActual;
    public int marchaActual = 0; // 0 = Neutro (N), 1 a 5 = Marchas

    [Header("Rangos de Velocidad por Marcha (N, 1ª, 2ª, 3ª, 4ª, 5ª)")]
    public float[] minVelocidadPorMarcha = { 0f, 0f, 25f, 60f, 100f, 150f };
    public float[] maxVelocidadPorMarcha = { 0f, 40f, 80f, 130f, 180f, 250f };

    private Rigidbody2D rb;
    private bool estaAcelerando;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rpmActual = rpmMinimas;
    }

    void Update()
    {
        // Detectamos el teclado en Update para una respuesta inmediata
        estaAcelerando = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        GestionarCambios();
        CalcularTelemetria();
    }

    void FixedUpdate()
    {
        ManejarFisicasMotor();
    }

    void GestionarCambios()
    {
        // Subir marcha con 'E'
        if (Input.GetKeyDown(KeyCode.E) && marchaActual < 5)
        {
            marchaActual++;
        }

        // Bajar marcha con 'Q'
        if (Input.GetKeyDown(KeyCode.Q) && marchaActual > 0)
        {
            marchaActual--;
        }
    }

    void ManejarFisicasMotor()
    {
        if (marchaActual > 0)
        {
            float maxVel = maxVelocidadPorMarcha[marchaActual];

            if (estaAcelerando)
            {
                // Acelera solo si no ha alcanzado la velocidad máxima de la marcha actual
                if (velocidadKmh < maxVel)
                {
                    rb.AddForce(Vector2.right * fuerzaMotor);
                }
            }
            else
            {
                // AL SOLTAR 'W': Aplica freno motor efectivo reduciendo la velocidad a 0
                rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, desaceleracion * Time.fixedDeltaTime);
            }

            // Retención si la velocidad supera el límite de la marcha seleccionada
            if (velocidadKmh > maxVel)
            {
                rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, rb.linearVelocity.normalized * (maxVel / 3.6f), Time.fixedDeltaTime * 20f);
            }
        }
    }

    void CalcularTelemetria()
    {
        velocidadKmh = rb.linearVelocity.magnitude * 3.6f;
        float targetRPM = rpmMinimas;

        if (marchaActual == 0) // NEUTRO
        {
            // En Neutro: sube a 9000 al acelerar y vuelve a 1500 al soltar
            targetRPM = estaAcelerando ? rpmMaximas : rpmMinimas;
        }
        else // EN MARCHA (1ª a 5ª)
        {
            float minVel = minVelocidadPorMarcha[marchaActual];
            float maxVel = maxVelocidadPorMarcha[marchaActual];

            // Porcentaje de velocidad en la marcha actual
            float progresoMarcha = Mathf.InverseLerp(minVel, maxVel, velocidadKmh);
            targetRPM = Mathf.Lerp(rpmMinimas, rpmMaximas, progresoMarcha);
        }

        // Transición lineal limpia para las RPM
        rpmActual = Mathf.MoveTowards(rpmActual, targetRPM, velocidadAgujaRPM * Time.deltaTime);
    }

    public float GetVelocidadActual()
    {
        return velocidadKmh;
    }
}