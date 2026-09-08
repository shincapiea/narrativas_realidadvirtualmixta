using UnityEngine;

public class SecuenciaJugoVR : MonoBehaviour
{
    public int step = 0;
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip errorClip;

    public void OnGrabbedNaranja()
    {
        if (step == 0) {
            step = 1;
            PlaySuccess();
            Debug.Log("Paso 1 completado: Naranja agarrada.");
        }
    }

    public void OnCutNaranja()
    {
        if (step == 1) {
            step = 2;
            PlaySuccess();
            Debug.Log("Paso 2 completado: Naranja cortada.");
        } else if (step == 0) {
            PlayError();
            Debug.Log("Error: Agarra la naranja primero.");
        }
    }

    public void OnPutInLicuadora()
    {
        if (step == 2) {
            step = 3;
            PlaySuccess();
            Debug.Log("Paso 3 completado: Naranjas en la licuadora.");
        } else {
            PlayError();
            Debug.Log("Error: Debes cortar la naranja primero.");
        }
    }

    public void OnActivateLicuadora()
    {
        if (step == 3) {
            step = 4;
            PlaySuccess();
            Debug.Log("Paso 4 completado: Jugo listo.");
        } else {
            PlayError();
            Debug.Log("Error: No puedes activar sin naranjas.");
        }
    }

    void PlaySuccess() { 
        if(audioSource && successClip) audioSource.PlayOneShot(successClip); 
    }
    void PlayError() { 
        if(audioSource && errorClip) audioSource.PlayOneShot(errorClip); 
    }
}
