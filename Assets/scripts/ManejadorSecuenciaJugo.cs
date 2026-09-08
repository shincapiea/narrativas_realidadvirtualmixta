using UnityEngine;


public class ManejadorSecuenciaJugo : MonoBehaviour
{
    public AudioClip successSound;
    public AudioClip errorSound;
    private AudioSource audioSource;

    private int currentStep = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;

        // Auto-hookup
        HookUp("cuchillo", OnGrabbedCuchillo);
        HookUp("cuchillointeractuable", OnGrabbedCuchillo); // En caso de que se llame as�

        HookUp("media naranja 1", OnGrabbedNaranja);
        HookUp("naranjainteractuable", OnGrabbedNaranja); // O si hay otra
        HookUp("media naranja 2", OnGrabbedNaranja);
    }

    private void HookUp(string objName, UnityEngine.Events.UnityAction action)
    {
        GameObject obj = GameObject.Find(objName);
        if (obj != null) {
            var grab = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grab != null) {
                // Remove first just in case
                grab.selectEntered.RemoveListener((args) => action());
                grab.selectEntered.AddListener((args) => action());
            }
        }
    }

    public void OnGrabbedCuchillo()
    {
        if (currentStep == 0) {
            currentStep = 1;
            PlaySound(successSound);
        }
    }

    public void OnGrabbedNaranja()
    {
        if (currentStep == 1) {
            currentStep = 2;
            PlaySound(successSound);
        } else if (currentStep == 0) {
            PlaySound(errorSound);
        }
    }

    public void OnNaranjaEnLicuadora()
    {
        if (currentStep == 2) {
            currentStep = 3;
            PlaySound(successSound);
        } else if (currentStep < 2) {
            PlaySound(errorSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null) {
            audioSource.PlayOneShot(clip);
        }
    }
}
