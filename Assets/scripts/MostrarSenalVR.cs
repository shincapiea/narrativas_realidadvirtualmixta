using UnityEngine;

public class MostrarSenalVR : MonoBehaviour
{
    [Tooltip("La señal visual que aparecerá cuando agarres este objeto")]
    public GameObject senalAMostrar;

    private Component interactable;
    private System.Reflection.PropertyInfo isSelectedProp;

    void Start()
    {
        // Buscamos dinámicamente el componente de agarre de XR
        var scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script.GetType().Name.Contains("XRGrabInteractable"))
            {
                interactable = script;
                isSelectedProp = script.GetType().GetProperty("isSelected");
                break;
            }
        }
        
        // Apagamos la señal visualmente al iniciar
        SetVisualsActive(false);
    }

    void Update()
    {
        if (interactable != null && isSelectedProp != null && senalAMostrar != null)
        {
            bool agarrado = (bool)isSelectedProp.GetValue(interactable);
            
            // Actualizamos la visibilidad en cada frame (se podría optimizar, pero es seguro)
            SetVisualsActive(agarrado);
        }
    }

    private void SetVisualsActive(bool active)
    {
        if (senalAMostrar == null) return;

        // Apagar todos los renderers (mallas 3D)
        var renderers = senalAMostrar.GetComponentsInChildren<Renderer>();
        foreach(var r in renderers) r.enabled = active;

        // Apagar todos los canvases (UI)
        var canvases = senalAMostrar.GetComponentsInChildren<Canvas>();
        foreach(var c in canvases) c.enabled = active;
    }
}
