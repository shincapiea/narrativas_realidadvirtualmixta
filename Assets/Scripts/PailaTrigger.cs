using UnityEngine;

public class PailaTrigger : MonoBehaviour
{
    public SimpleCookSequence sequenceManager;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the orange
        if (other.name.ToLower().Contains("naranja"))
        {
            if (sequenceManager != null)
            {
                sequenceManager.OnOrangeInPaila();
            }
        }
    }
}
