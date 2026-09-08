using UnityEngine;

public class FijarPerillaVR : MonoBehaviour
{
    public Transform puerta;
    private Vector3 offsetLocal;

    void Start() {
        if (puerta != null) {
            transform.SetParent(puerta, true);
            offsetLocal = transform.localPosition;
        }
    }

    void LateUpdate() {
        if (puerta != null) {
            // Fija la posición local para que no se arranque de la puerta
            transform.localPosition = offsetLocal;
        }
    }
}
