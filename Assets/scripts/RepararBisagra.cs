using UnityEngine;

public class RepararBisagra : MonoBehaviour
{
    void Start()
    {
        HingeJoint doorHinge = GetComponent<HingeJoint>();
        if (doorHinge != null) {
            // Como la puerta está rotada -90 en X, su eje "Arriba" en el mundo local es Z (forward)
            doorHinge.axis = Vector3.forward;
        }
    }
}
