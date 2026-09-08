using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PerillaMagicaVR : MonoBehaviour
{
    public PuertaMagicaVR puertaMagica;
    public AudioClip knobSound;
    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private bool isTurning = false;
    private float targetY = 60f; // Girar 60 grados
    private float currentY = 0f;
    private bool hasTurned = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;

        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable == null) interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        
        interactable.selectEntered.RemoveAllListeners();
        interactable.selectEntered.AddListener(OnKnobGrabbed);
    }

    private void OnKnobGrabbed(SelectEnterEventArgs args)
    {
        if (!hasTurned) {
            isTurning = true;
            hasTurned = true;
            
            if (knobSound != null) audioSource.PlayOneShot(knobSound);
            if (puertaMagica != null) puertaMagica.isUnlocked = true;
            
            Debug.Log("Perilla girada. Puerta desbloqueada.");
        }
    }

    void Update()
    {
        if (isTurning) {
            currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * 5f);
            
            // Asumimos que la perilla gira en su eje Y o Z (usaremos Y que es com�n para perillas)
            transform.localRotation = Quaternion.Euler(0f, currentY, 0f);
            
            if (Mathf.Abs(targetY - currentY) < 0.1f) {
                isTurning = false;
            }
        }
    }
}
