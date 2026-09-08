using UnityEngine;

public class StartUIVR : MonoBehaviour
{
    public float delay = 5f;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
