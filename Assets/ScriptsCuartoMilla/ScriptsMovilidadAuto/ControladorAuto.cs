using UnityEngine;

public class ControladorAuto : MonoBehaviour
{
    [Header("Configuración de Aceleración y Motor")]
    public float fuerzaMotor = 85f;
    public float fuerzaFreno = 70f; // Ajusta en el Inspector la potencia del frenado
    public float sensibilidadPedal = 2.5f;
    public float resistenciaAire = 1.5f;

    [Header("Rangos por Marcha")]
    public float[] maxVelocidadPorMarcha = { 0f, 40f, 80f, 130f, 180f, 250f };
    public float[] fuerzaPorMarcha = { 0f, 1.4f, 1.1f, 0.85f, 0.65f, 0.45f };

    [Header("Configuración de RPM")]
    public float rpmMinimas = 1000f;
    public float rpmMaximas = 11000f;
    public float velocidadAgujaRPM = 5000f;

    public float velocidadKmh;
    public float rpmActual;
    public int marchaActual = 0;

    private Rigidbody2D rb;
    private bool estaAcelerando;
    private bool estaFrenando;
    private float pedalAcelerador = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rpmActual = rpmMinimas;
    }

    void Update()
    {
        estaAcelerando = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        estaFrenando = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        float objetivoPedal = estaAcelerando ? 1f : 0f;
        pedalAcelerador = Mathf.MoveTowards(pedalAcelerador, objetivoPedal, sensibilidadPedal * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) && marchaActual < maxVelocidadPorMarcha.Length - 1) marchaActual++;
        if (Input.GetKeyDown(KeyCode.Q) && marchaActual > 0) marchaActual--;

        CalcularRPM();
    }

    void FixedUpdate()
    {
        velocidadKmh = rb.linearVelocity.magnitude * 3.6f;

        // 1. FRENO ACTIVO (Tecla S)
        if (estaFrenando)
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, fuerzaFreno * Time.fixedDeltaTime);
        }
        // 2. ACELERACIÓN / RETENCIÓN EN MARCHA
        else if (marchaActual > 0 && marchaActual < maxVelocidadPorMarcha.Length)
        {
            float maxVel = maxVelocidadPorMarcha[marchaActual];
            float multiplicadorTorque = fuerzaPorMarcha[marchaActual];
            float factorProgresion = Mathf.Clamp01(1f - (velocidadKmh / maxVel));

            float fuerzaEfectiva = fuerzaMotor * multiplicadorTorque * factorProgresion * pedalAcelerador;

            if (velocidadKmh < maxVel)
            {
                rb.AddForce(Vector2.right * fuerzaEfectiva, ForceMode2D.Force);
            }

            if (!estaAcelerando)
            {
                rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, resistenciaAire * Time.fixedDeltaTime);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, resistenciaAire * Time.fixedDeltaTime);
        }
    }

    void CalcularRPM()
    {
        float targetRPM = rpmMinimas;

        if (marchaActual == 0)
        {
            targetRPM = Mathf.Lerp(rpmMinimas, rpmMaximas, pedalAcelerador);
        }
        else
        {
            float minVel = (marchaActual > 1) ? maxVelocidadPorMarcha[marchaActual - 1] * 0.4f : 0f;
            float maxVel = maxVelocidadPorMarcha[marchaActual];

            float progresoMarcha = Mathf.InverseLerp(minVel, maxVel, velocidadKmh);
            targetRPM = Mathf.Lerp(rpmMinimas, rpmMaximas, progresoMarcha);
        }

        rpmActual = Mathf.MoveTowards(rpmActual, targetRPM, velocidadAgujaRPM * Time.deltaTime);
    }

    public float GetVelocidadActual()
    {
        return velocidadKmh;
    }
}