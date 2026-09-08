using UnityEngine;

public class ZonaLicuadora : MonoBehaviour
{
    public ManejadorSecuenciaJugo manejador;

    // Funciona si usan un Trigger (caja invisible)
    void OnTriggerEnter(Collider other)
    {
        ProcesarContacto(other.gameObject);
    }

    // Funciona si simplemente choca fisicamente contra el objeto
    void OnCollisionEnter(Collision collision)
    {
        ProcesarContacto(collision.gameObject);
    }

    private void ProcesarContacto(GameObject obj)
    {
        if (obj.name.Contains("naranja"))
        {
            if (manejador != null) {
                manejador.OnNaranjaEnLicuadora();
            }
            
            obj.SetActive(false);
            Debug.Log("La naranja tocó la licuadora y fue procesada.");
        }
    }
}
