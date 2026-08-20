using UnityEngine;

public class ConfiguradorPista : MonoBehaviour
{
    [Header("Configuración de Pista")]
    public Transform lineaDeMeta;
    public Transform lineaDeSalida;

    [Header("Generación del Auto")]
    [Tooltip("Arrastra aquí los 3 Prefabs de los autos (E36, E46, M2)")]
    public GameObject[] prefabsAutos;

    [Tooltip("Arrastra el script LineaDeMeta de tu escena")]
    public LineaDeMeta scriptLineaDeMeta;

    [Header("Cámara")]
    [Tooltip("Arrastra aquí el script SeguimientoCamara que tiene la Main Camera")]
    public SeguimientoCamara scriptCamara;

    private void Awake()
    {
        GenerarAutoElegido();
    }

    private void Start()
    {
        ConfigurarDistanciaPista();
    }

    private void GenerarAutoElegido()
    {
        if (prefabsAutos == null || prefabsAutos.Length == 0 || lineaDeSalida == null) return;

        // Leer índice guardado en el menú
        int indexAuto = PlayerPrefs.GetInt("AutoElegidoIndex", 0);

        if (indexAuto < 0 || indexAuto >= prefabsAutos.Length) indexAuto = 0;

        // Instancia el auto seleccionado
        GameObject autoInstanciado = Instantiate(prefabsAutos[indexAuto], lineaDeSalida.position, Quaternion.identity);

        // 1. Asigna el auto a la Linea de Meta
        if (scriptLineaDeMeta != null)
        {
            scriptLineaDeMeta.auto = autoInstanciado.transform;
        }

        // 2. Asigna el auto a la variable 'objetivo' de tu SeguimientoCamara
        if (scriptCamara != null)
        {
            scriptCamara.objetivo = autoInstanciado.transform;
        }
    }

    private void ConfigurarDistanciaPista()
    {
        float distanciaMetros = PlayerPrefs.GetFloat("DistanciaMetros", 402.33f);

        if (lineaDeMeta != null && lineaDeSalida != null)
        {
            Vector3 posicionMeta = lineaDeMeta.position;
            posicionMeta.x = lineaDeSalida.position.x + distanciaMetros;
            lineaDeMeta.position = posicionMeta;
        }
    }
}