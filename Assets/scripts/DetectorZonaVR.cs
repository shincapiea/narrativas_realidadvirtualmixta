using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DetectorZonaVR : MonoBehaviour
{
    [Tooltip("El objeto que el jugador debe traer a esta zona (ej. La paila)")]
    public GameObject objetoEsperado;

    [Tooltip("Lo que pasará cuando el objeto llegue (Sonido, apagar señal, etc.)")]
    public UnityEvent alCompletarExito;

    private bool yaActivado = false;

    private void OnTriggerStay(Collider other)
    {
        if (yaActivado || objetoEsperado == null) return;

        // Comprobamos si el objeto que colisiona es el esperado, o si es un colisionador hijo del esperado
        if (other.gameObject == objetoEsperado || other.transform.IsChildOf(objetoEsperado.transform))
        {
            // Verificar si el objeto está siendo agarrado actualmente por el jugador
            bool estaAgarrado = false;
            var scripts = other.GetComponentsInParent<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script.GetType().Name.Contains("XRGrabInteractable"))
                {
                    var prop = script.GetType().GetProperty("isSelected");
                    if (prop != null)
                    {
                        estaAgarrado = (bool)prop.GetValue(script);
                    }
                }
            }

            // Solo activamos el éxito si el objeto NO está agarrado (lo dejó caer en la zona)
            if (!estaAgarrado)
            {
                yaActivado = true;
                
                // Ejecutamos todo lo que configures en el Inspector
                alCompletarExito.Invoke();
                
                // Apagamos el colisionador para que no suene dos veces
                var col = GetComponent<Collider>();
                if (col != null) col.enabled = false;
            }
        }
    }
}