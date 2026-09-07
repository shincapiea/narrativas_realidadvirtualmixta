using UnityEngine;

public class SimpleCookSequence : MonoBehaviour
{
    public int step = 0;
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip errorClip;

    public void OnGrabbedOrange()
    {
        if (step == 0) {
            step = 1;
            PlaySuccess();
            Debug.Log("¡Paso 1 completado! Naranja agarrada.");
        }
    }

    public void OnGrabbedPaila()
    {
        if (step == 0) {
            PlayError();
            Debug.Log("¡Error! Debes agarrar la naranja primero.");
        }
    }

    public void OnOrangeInPaila()
    {
        if (step == 1) {
            step = 2;
            PlaySuccess();
            Debug.Log("¡Paso 2 completado! Naranja en la paila.");
        } else if (step == 0) {
            PlayError();
            Debug.Log("¡Error! La naranja no puede ir a la paila si no la has agarrado primero (o en el orden incorrecto).");
        }
    }

    void PlaySuccess() { 
        if(audioSource && successClip) audioSource.PlayOneShot(successClip); 
    }
    void PlayError() { 
        if(audioSource && errorClip) audioSource.PlayOneShot(errorClip); 
    }
}
