using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MostrarSenalVR : MonoBehaviour
{
    public GameObject senalAMostrar;
    public GameObject flechaIndicadora;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
        
        if (senalAMostrar) senalAMostrar.SetActive(false);
        if (flechaIndicadora) flechaIndicadora.SetActive(true);
    }

    void OnGrabbed(SelectEnterEventArgs args) {
        if (args.interactorObject.GetType().Name.Contains("Socket")) return;
        
        if (senalAMostrar) senalAMostrar.SetActive(true);
        if (flechaIndicadora) flechaIndicadora.SetActive(false);
        if (audioSource) audioSource.Play(); // Play sound when grabbed
    }

    void OnReleased(SelectExitEventArgs args) {
        if (senalAMostrar) senalAMostrar.SetActive(false);
        if (flechaIndicadora) flechaIndicadora.SetActive(true);
    }
}
