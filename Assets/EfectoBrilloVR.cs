using UnityEngine;

public class EfectoBrilloVR : MonoBehaviour
{
    [Tooltip("Asigna aquí el material brillante que creaste")]
    public Material materialBrillante;

    private Material[] materialesOriginales;
    private MeshRenderer meshRenderer;

    void Start()
    {
        // Buscamos la malla de la paila
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            // Guardamos los materiales originales (acciaio, COLTELLO, etc.)
            materialesOriginales = meshRenderer.materials;
        }
    }

    // Esta función la conectaremos al evento de agarrar
    public void EncenderBrillo()
    {
        if (meshRenderer != null && materialBrillante != null)
        {
            // Reemplazamos todos los materiales por el brillante
            Material[] temporal = new Material[materialesOriginales.Length];
            for (int i = 0; i < temporal.Length; i++)
            {
                temporal[i] = materialBrillante;
            }
            meshRenderer.materials = temporal;
        }
    }

    // Esta función la conectaremos al evento de soltar
    public void ApagarBrillo()
    {
        if (meshRenderer != null && materialesOriginales != null)
        {
            // Restauramos los materiales originales exactos
            meshRenderer.materials = materialesOriginales;
        }
    }
}