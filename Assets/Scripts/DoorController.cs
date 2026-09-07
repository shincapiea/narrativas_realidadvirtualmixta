using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public HingeJoint doorHinge;
    public HingeJoint knobHinge;
    public Text instructionText;
    
    private bool isUnlocked = false;

    void Start()
    {
        if (doorHinge != null) {
            JointLimits limits = doorHinge.limits;
            limits.min = 0;
            limits.max = 0;
            doorHinge.limits = limits;
            
            Rigidbody rb = doorHinge.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true; // Lock physics to prevent explosion
        }
    }

    void Update()
    {
        if (instructionText != null && Camera.main != null)
        {
            Transform canvasTransform = instructionText.canvas.transform;
            canvasTransform.rotation = Quaternion.LookRotation(canvasTransform.position - Camera.main.transform.position);
        }

        if (!isUnlocked && knobHinge != null)
        {
            if (Mathf.Abs(knobHinge.angle) > 40f)
            {
                UnlockAndOpenDoor();
            }
        }
    }
    
    void UnlockAndOpenDoor()
    {
        isUnlocked = true;
        
        if (doorHinge != null) {
            Rigidbody rb = doorHinge.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false; // Allow movement
            
            JointLimits limits = doorHinge.limits;
            limits.max = 90f;
            limits.min = -10f; 
            doorHinge.limits = limits;
            
            JointSpring spring = doorHinge.spring;
            spring.spring = 200f;
            spring.targetPosition = 85f;
            doorHinge.spring = spring;
            doorHinge.useSpring = true;
        }
        
        if (instructionText != null)
        {
            instructionText.text = "¡PUERTA ABIERTA!";
            instructionText.color = Color.green;
            Destroy(instructionText.canvas.gameObject, 3f);
        }
    }
}
