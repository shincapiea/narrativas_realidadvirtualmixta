using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EfectoBrilloVR : MonoBehaviour
{
    public Material materialBrillante;
    private Material[] materialesOriginales;
    private MeshRenderer meshRenderer;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            materialesOriginales = meshRenderer.materials;
        }

        if (materialBrillante == null)
        {
            materialBrillante = Resources.Load<Material>("materialBrillante");
        }

        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args) {
        // Ignorar si es un socket
        if (args.interactorObject.GetType().Name.Contains("Socket")) return;
        EncenderBrillo();
    }

    void OnReleased(SelectExitEventArgs args) {
        // Si lo soltamos, o si un socket lo agarra, apagamos el brillo
        ApagarBrillo();
    }

    public void EncenderBrillo()
    {
        if (meshRenderer != null && materialBrillante != null)
        {
            Material[] temporal = new Material[materialesOriginales.Length];
            for (int i = 0; i < temporal.Length; i++)
                temporal[i] = materialBrillante;
            meshRenderer.materials = temporal;
        }
    }

    public void ApagarBrillo()
    {
        if (meshRenderer != null && materialesOriginales != null)
        {
            meshRenderer.materials = materialesOriginales;
        }
    }
}
