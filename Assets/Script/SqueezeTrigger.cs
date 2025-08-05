using UnityEngine;

public class SqueezeTrigger : MonoBehaviour
{
    public bool isSqueezed = false;  // Boolean to check if the trigger is pressed

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isSqueezed = true;  // Trigger is pressed
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isSqueezed = false;  // Trigger is released
        }
    }
}

