using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DetectorJugadorVR : MonoBehaviour
{
    [Tooltip("El tag del jugador (generalmente 'Player' o 'MainCamera')")]
    public string tagJugador = "Player";

    [Tooltip("El objeto de las instrucciones que se va a encender")]
    public GameObject instrucciones;

    [Tooltip("Eventos extra opcionales")]
    public UnityEvent alLlegarJugador;

    private bool yaActivado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (yaActivado) return;

        // Comprobamos si lo que entró tiene el tag del jugador
        if (other.CompareTag(tagJugador) || other.name.Contains("XR Origin") || other.name.Contains("Main Camera") || other.name.Contains("Camera"))
        {
            yaActivado = true;
            
            if (instrucciones != null)
            {
                instrucciones.SetActive(true);
            }
            
            alLlegarJugador.Invoke();
            
            // Opcional: apagar la señalética de inicio
            gameObject.SetActive(false);
        }
    }
}
