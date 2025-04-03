using UnityEngine;

public class LasMatematicas : MonoBehaviour
{
    public static LasMatematicas Instance;
    [SerializeField] private GameObject Asteroide1;
    [SerializeField] private GameObject Asteroide2;
    [SerializeField] private GameObject Asteroide3;
    [SerializeField] private GameObject UFO;

    private float distanceAsteroide1;
    private float distanceAsteroide2;
    private float distanceAsteroide3;

    private Vector3 previousUFOPosition;

    private Vector3 deltaAsteroide1 = new Vector3(0.01f, 0, 0);
    private Vector3 deltaAsteroide2 = new Vector3(0, 0.01f, 0);
    private Vector3 deltaAsteroide3 = new Vector3(0, 0, 0.01f);

    void Start()
    {
        previousUFOPosition = UFO.transform.position;
        UpdateDistances();
        PrintDistancesToConsole();
    }

    void Update()
    {
        MoveAsteroids();
        UpdateDistances();
        PrintDistancesToConsole();
        CheckCollisions();
    }

    private void UpdateDistances()
    {
        distanceAsteroide1 = CustomDistance(Asteroide1.transform.position, UFO.transform.position);
        distanceAsteroide2 = CustomDistance(Asteroide2.transform.position, UFO.transform.position);
        distanceAsteroide3 = CustomDistance(Asteroide3.transform.position, UFO.transform.position);
    }

    private void PrintDistancesToConsole()
    {
        Debug.Log("Distancia a Asteroide 1: " + distanceAsteroide1.ToString("F2"));
        Debug.Log("Distancia a Asteroide 2: " + distanceAsteroide2.ToString("F2"));
        Debug.Log("Distancia a Asteroide 3: " + distanceAsteroide3.ToString("F2"));
    }

    public float CustomDistance(Vector3 pos1, Vector3 pos2)
    {
        float x = pos1.x - pos2.x;
        float y = pos1.y - pos2.y;
        float z = pos1.z - pos2.z;

        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    public Vector3 CustomTranslate(Vector3 pos, Vector3 delta)
    {
        return new Vector3(pos.x + delta.x, pos.y + delta.y, pos.z + delta.z);
    }
    public Vector3 RotX(float angulo, Vector3 pos)
    {
        // Rotación sobre el eje X (Pitch)
        float rad = Mathf.Deg2Rad * angulo; // Convertir a radianes
        float y = pos.y * Mathf.Cos(rad) - pos.z * Mathf.Sin(rad);
        float z = pos.y * Mathf.Sin(rad) + pos.z * Mathf.Cos(rad);
        return new Vector3(pos.x, y, z);
    }

    public Vector3 RotY(float angulo, Vector3 pos)
    {
        // Rotación sobre el eje Y (Yaw)
        float rad = Mathf.Deg2Rad * angulo; // Convertir a radianes
        float x = pos.x * Mathf.Cos(rad) + pos.z * Mathf.Sin(rad);
        float z = pos.z * Mathf.Cos(rad) - pos.x * Mathf.Sin(rad);
        return new Vector3(x, pos.y, z);
    }
    public Vector3 RotZ(float angulo, Vector3 pos)
    {
        float rad = Mathf.Deg2Rad * angulo; // Convertir a radianes para cálculos trigonométricos
        float x = pos.x * Mathf.Cos(rad) - pos.y * Mathf.Sin(rad);
        float y = pos.y * Mathf.Cos(rad) + pos.x * Mathf.Sin(rad);
        return new Vector3(x, y, pos.z);
    }

    private void MoveAsteroids()
    {
        Asteroide1.transform.position = CustomTranslate(Asteroide1.transform.position, deltaAsteroide1);
        Asteroide2.transform.position = CustomTranslate(Asteroide2.transform.position, deltaAsteroide2);
        Asteroide3.transform.position = CustomTranslate(Asteroide3.transform.position, deltaAsteroide3);
    }

    private void CheckCollisions()
    {
        if (IsColliding(Asteroide1.transform.position, UFO.transform.position))
        {
            Debug.Log("Explotaste Boom con Asteroide 1");
        }
        if (IsColliding(Asteroide2.transform.position, UFO.transform.position))
        {
            Debug.Log("Explotaste Boom con Asteroide 2");
        }
        if (IsColliding(Asteroide3.transform.position, UFO.transform.position))
        {
            Debug.Log("Explotaste Boom con Asteroide 3");
        }
    }

    private bool IsColliding(Vector3 pos1, Vector3 pos2)
    {
        // Define una distancia mínima para considerar una colisión
        float collisionDistance = 0.5f; // Ajusta este valor según el tamaño de los objetos
        return CustomDistance(pos1, pos2) < collisionDistance;
    }
}