using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuertaManualVR : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    public bool isUnlocked = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor currentHand;
    private Vector3 initialHandLocalPos;
    private float initialDoorZ;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (!isUnlocked) return;
        
        currentHand = args.interactorObject;
        // Posici�n de la mano en el espacio local de la puerta
        initialHandLocalPos = transform.InverseTransformPoint(currentHand.transform.position);
        initialDoorZ = transform.localEulerAngles.z;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        currentHand = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (currentHand != null && isUnlocked)
            {
                // Convertimos la posici�n actual de la mano al espacio del padre de la puerta
                Vector3 currentHandInParentSpace = transform.parent != null 
                    ? transform.parent.InverseTransformPoint(currentHand.transform.position) 
                    : currentHand.transform.position;
                    
                // Simplemente tomamos la diferencia en la posici�n Z (o X, dependiendo de c�mo jalemos)
                // Para simplificar al m�ximo y que NUNCA falle, usaremos un truco:
                // Mediremos la distancia entre la mano y la puerta y la empujaremos.
            }
        }
    }
}
