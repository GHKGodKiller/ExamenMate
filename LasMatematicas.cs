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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inicializar distancias si es necesario
        distanceAsteroide1 = Distance(Asteroide1.transform.position, UFO.transform.position);
        distanceAsteroide2 = Distance(Asteroide2.transform.position, UFO.transform.position);
        distanceAsteroide3 = Distance(Asteroide3.transform.position, UFO.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceAsteroide1 = Distance(Asteroide1.transform.position, UFO.transform.position);
        distanceAsteroide2 = Distance(Asteroide2.transform.position, UFO.transform.position);
        distanceAsteroide3 = Distance(Asteroide3.transform.position, UFO.transform.position);
    }

    public float Distance(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1, pos2);
    }
}
