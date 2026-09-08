using UnityEngine;

public class FollowObjectVR : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0.2f, 0);

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            // Optionally make it face the camera:
            if (Camera.main != null)
            {
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            }
        }
    }
}
