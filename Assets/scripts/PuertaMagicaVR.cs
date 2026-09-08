using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuertaMagicaVR : MonoBehaviour
{
    public bool isUnlocked = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private bool isOpening = false;
    private float targetZ = 100f; // Abre 100 grados
    private float currentZ = 0f;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable == null) interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        
        // Limpiamos los eventos previos para evitar dobles llamadas
        interactable.selectEntered.RemoveAllListeners();
        interactable.selectEntered.AddListener(OnDoorGrabbed);
    }

    private void OnDoorGrabbed(SelectEnterEventArgs args)
    {
        if (isUnlocked && !isOpening) {
            isOpening = true;
            Debug.Log("Abriendo puerta m�gicamente...");
        } else if (!isUnlocked) {
            Debug.Log("La puerta est� bloqueada. Gira la perilla primero.");
        }
    }

    void Update()
    {
        if (isOpening) {
            // Rotar suavemente en el eje Z local (que act�a como Y vertical por el -90 en X)
            currentZ = Mathf.Lerp(currentZ, targetZ, Time.deltaTime * 3f);
            
            // Mantenemos X en -90, Y en 0, y animamos Z
            transform.localRotation = Quaternion.Euler(-90f, 0f, currentZ);
            
            if (Mathf.Abs(targetZ - currentZ) < 0.1f) {
                isOpening = false; // Termin� de abrir
            }
        }
    }
}
