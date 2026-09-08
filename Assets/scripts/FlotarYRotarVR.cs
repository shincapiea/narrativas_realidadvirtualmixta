using UnityEngine;

public class FlotarYRotarVR : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0.3f, 0); // Altura base
    public float floatAmp = 0.03f; // Sube y baja 3cm
    public float floatSpeed = 3f;

    private float startY;

    void Start() {
        startY = offset.y;
    }

    void Update()
    {
        if (target != null)
        {
            // Movimiento arriba y abajo (bobbing)
            float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatAmp;
            Vector3 currentOffset = new Vector3(offset.x, newY, offset.z);
            
            transform.position = target.position + currentOffset;
            
            // Fija la rotación para que siempre apunte hacia abajo (rotando 180 grados en X o Z dependiendo del modelo)
            // Si el modelo original apunta hacia arriba (Y positivo), darle la vuelta 180 en X lo hará apuntar hacia abajo.
            transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        }
    }
}
