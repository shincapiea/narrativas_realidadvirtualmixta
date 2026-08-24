using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DetectorZonaVR : MonoBehaviour
{
    [Tooltip("El objeto que el jugador debe traer a esta zona (ej. La paila)")]
    public GameObject objetoEsperado;

    [Tooltip("Lo que pasará cuando el objeto llegue (Sonido, apagar señal, etc.)")]
    public UnityEvent alCompletarExito;

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que choca con esta zona es la paila...
        if (other.gameObject == objetoEsperado)
        {
            // Ejecutamos todo lo que configures en el Inspector
            alCompletarExito.Invoke();
            
            // Opcional: Apagar este detector para que no suene 2 veces
            gameObject.SetActive(false); 
        }
    }
}