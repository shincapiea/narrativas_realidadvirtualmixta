using UnityEngine;

public class HideAfterTime : MonoBehaviour
{
    public float timeToHide = 15f;

    void Start()
    {
        Destroy(gameObject, timeToHide);
    }
}
