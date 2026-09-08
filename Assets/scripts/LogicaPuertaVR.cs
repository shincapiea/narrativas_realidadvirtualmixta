using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LogicaPuertaVR : MonoBehaviour
{
    public HingeJoint doorHinge;
    public HingeJoint knobHinge;
    
    public AudioClip knobSound;
    public AudioClip doorSound;
    
    private AudioSource audioSource;
    private bool isUnlocked = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // Sonido 3D
        
        // La puerta empieza bloqueada
        if (doorHinge != null) {
            JointLimits limits = doorHinge.limits;
            limits.min = 0;
            limits.max = 0; // Bloqueada
            doorHinge.limits = limits;
        }
    }

    void Update()
    {
        if (!isUnlocked && knobHinge != null)
        {
            // Verificamos el ángulo del Hinge de la perilla
            float angle = knobHinge.angle;
            
            // Si lo gira más de 45 grados en cualquier dirección, se abre
            if (Mathf.Abs(angle) > 45f)
            {
                DesbloquearPuerta();
            }
        }
    }

    private void DesbloquearPuerta()
    {
        isUnlocked = true;
        
        if (knobSound != null) audioSource.PlayOneShot(knobSound);
        
        if (doorHinge != null) {
            JointLimits limits = doorHinge.limits;
            limits.min = 0;
            limits.max = 120; // Se puede abrir hasta 120 grados
            doorHinge.limits = limits;
            
            // Si también queremos que suene al abrirse, podemos poner el sonido aquí
            if (doorSound != null) audioSource.PlayOneShot(doorSound);
        }
        
        Debug.Log("¡Puerta Desbloqueada!");
    }
}
