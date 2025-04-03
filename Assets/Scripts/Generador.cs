using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject Asteroide1; // Prefab del asteroide
    public GameObject UFO; // Referencia al UFO
    public int cantidadAsteroides = 10; // Cantidad de asteroides a generar
    public float rangoDeGeneracion = 10f; // Rango en el que se generarán los asteroides
    public float velocidadAsteroides = 0.5f; // Velocidad de movimiento de los asteroides

    private GameObject[] asteroides;
    private Vector3[] direccionesAsteroides;

    void Start()
    {
        GenerarAsteroides();
    }

    void Update()
    {
        MoverAsteroides();
        VerificarColisiones();
    }

    private void GenerarAsteroides()
    {
        asteroides = new GameObject[cantidadAsteroides];
        direccionesAsteroides = new Vector3[cantidadAsteroides];

        for (int i = 0; i < cantidadAsteroides; i++)
        {
            Vector3 posicionAleatoria = new Vector3(
                Random.Range(-rangoDeGeneracion, rangoDeGeneracion),
                Random.Range(-rangoDeGeneracion, rangoDeGeneracion),
                Random.Range(-rangoDeGeneracion, rangoDeGeneracion)
            );

            asteroides[i] = Instantiate(Asteroide1, posicionAleatoria, Quaternion.identity);
            direccionesAsteroides[i] = Random.onUnitSphere; // Dirección aleatoria
        }
    }

    private void MoverAsteroides()
    {
        for (int i = 0; i < asteroides.Length; i++)
        {
            asteroides[i].transform.position += direccionesAsteroides[i] * velocidadAsteroides * Time.deltaTime;
        }
    }

    private void VerificarColisiones()
    {
        foreach (GameObject asteroide in asteroides)
        {
            if (Vector3.Distance(asteroide.transform.position, UFO.transform.position) < 0.5f)
            {
                Debug.Log("¡Colisión con un asteroide!");
            }
        }
    }
}