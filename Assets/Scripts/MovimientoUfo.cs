using UnityEngine;

public class MovimientoUfo : MonoBehaviour
{
    public static MovimientoUfo Instance;
    [SerializeField] private GameObject Asteroide1;
    [SerializeField] private GameObject Asteroide2;
    [SerializeField] private GameObject Asteroide3;
    [SerializeField] private GameObject UFO;

    private float distanceAsteroide1;
    private float distanceAsteroide2;
    private float distanceAsteroide3;

    private Vector3 deltaAsteroide1 = new Vector3(0.01f, 0, 0);
    private Vector3 deltaAsteroide2 = new Vector3(0, 0.01f, 0);
    private Vector3 deltaAsteroide3 = new Vector3(0, 0, 0.01f);

    private float moveSpeed = 5f;
    private float rotationSpeed = 50f;

    private float RotX = 0f; // Rotación arriba/abajo (Pitch)
    private float RotY = 0f; // Rotación izquierda/derecha (Yaw)

    void Start()
    {
        UpdateDistances();
        PrintDistancesToConsole();
    }

    void Update()
    {
        HandleMovement();
        MoveAsteroids();
        UpdateDistances();
        PrintDistancesToConsole();
    }

    private void HandleMovement()
    {
        // Movimiento hacia adelante o atrás
        float moveForward = Input.GetAxis("Vertical"); // W (+) / S (-)
        Vector3 moveDirection = UFO.transform.forward * moveForward;

        // Movimiento hacia arriba o abajo
        float moveUp = 0f;
        if (Input.GetKey(KeyCode.Space)) moveUp = 1f; // Subir
        if (Input.GetKey(KeyCode.LeftControl)) moveUp = -1f; // Bajar
        moveDirection += Vector3.up * moveUp;

        // Aplicar movimiento
        UFO.transform.position = CustomTranslate(UFO.transform.position, moveDirection * moveSpeed * Time.deltaTime);

        // Actualizar valores de RotY y RotX
        RotY = 0f; // Rotación sobre el eje Y
        RotX = 0f; // Rotación sobre el eje X

        if (Input.GetKey(KeyCode.A)) RotY = -1f; // Izquierda
        if (Input.GetKey(KeyCode.D)) RotY = 1f;  // Derecha

        if (Input.GetKey(KeyCode.Q)) RotX = -1f; // Inclinación hacia abajo
        if (Input.GetKey(KeyCode.E)) RotX = 1f;  // Inclinación hacia arriba

        // Aplicar rotaciones
        UFO.transform.Rotate(Vector3.up, RotY * rotationSpeed * Time.deltaTime, Space.World);
        UFO.transform.Rotate(Vector3.right, RotX * rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void MoveAsteroids()
    {
        Asteroide1.transform.position = CustomTranslate(Asteroide1.transform.position, deltaAsteroide1);
        Asteroide2.transform.position = CustomTranslate(Asteroide2.transform.position, deltaAsteroide2);
        Asteroide3.transform.position = CustomTranslate(Asteroide3.transform.position, deltaAsteroide3);
    }

    private void UpdateDistances()
    {
        distanceAsteroide1 = CustomDistance(Asteroide1.transform.position, UFO.transform.position);
        distanceAsteroide2 = CustomDistance(Asteroide2.transform.position, UFO.transform.position);
        distanceAsteroide3 = CustomDistance(Asteroide3.transform.position, UFO.transform.position);
    }

    private void PrintDistancesToConsole()
    {
        Debug.Log($"Distancia a Asteroide 1: {distanceAsteroide1:F2}");
        Debug.Log($"Distancia a Asteroide 2: {distanceAsteroide2:F2}");
        Debug.Log($"Distancia a Asteroide 3: {distanceAsteroide3:F2}");
    }

    private float CustomDistance(Vector3 pos1, Vector3 pos2)
    {
        float x = pos1.x - pos2.x;
        float y = pos1.y - pos2.y;
        float z = pos1.z - pos2.z;

        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    private Vector3 CustomTranslate(Vector3 pos, Vector3 delta)
    {
        return new Vector3(pos.x + delta.x, pos.y + delta.y, pos.z + delta.z);
    }
}