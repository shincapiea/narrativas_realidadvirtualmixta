using UnityEngine;

public class FijarBisagraVR : MonoBehaviour
{
    private Vector3 originalPos;
    
    void Start() {
        originalPos = transform.position;
    }

    void LateUpdate() {
        // Obliga a la puerta a quedarse en su posición original
        transform.position = originalPos;
    }
}
