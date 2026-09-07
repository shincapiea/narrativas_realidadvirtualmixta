using UnityEngine;

public class BouncingArrow : MonoBehaviour
{
    public float speed = 2f;
    public float height = 0.05f;
    
    private Vector3 initialWorldOffset;
    private Quaternion initialRotation;

    void Start()
    {
        if (transform.parent != null) {
            initialWorldOffset = transform.position - transform.parent.position;
        }
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            // Lock position above the parent, ignoring parent's rotation
            Vector3 basePos = transform.parent.position + initialWorldOffset;
            transform.position = basePos + new Vector3(0, Mathf.Sin(Time.time * speed) * height, 0);
            
            // Lock rotation so it never spins
            transform.rotation = initialRotation;
        }
    }
}
