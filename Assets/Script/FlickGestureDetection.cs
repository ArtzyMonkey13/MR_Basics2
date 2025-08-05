using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlickGestureDetection : MonoBehaviour
{
    public XRController controller; // The controller (usually left or right)
    public GameObject paintSplatterEffect; // The paint splatter effect prefab
    public float flickThreshold = 5.0f; // Velocity threshold for flick detection
    public float effectDuration = 2.0f; // Duration for the splatter effect

    private Vector3 previousPosition;
    private Vector3 velocity;
    private bool isFlicking = false;
    
    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<XRController>();
        }

        previousPosition = controller.transform.position;
    }

    void Update()
    {
        // Calculate the velocity based on position changes
        Vector3 currentPosition = controller.transform.position;
        velocity = (currentPosition - previousPosition) / Time.deltaTime;

        // Update previous position
        previousPosition = currentPosition;

        // Check if the velocity exceeds the flick threshold
        if (velocity.magnitude > flickThreshold)
        {
            if (!isFlicking)
            {
                isFlicking = true;
                TriggerPaintSplatter();
            }
        }
        else
        {
            isFlicking = false;
        }
    }

    void TriggerPaintSplatter()
    {
        // Instantiate the paint splatter effect at the controller's position
        GameObject splatter = Instantiate(paintSplatterEffect, controller.transform.position, Quaternion.identity);

        // Optionally add some randomness to the splatter direction/rotation
        splatter.transform.rotation = Quaternion.LookRotation(velocity.normalized);

        // Destroy the splatter effect after a set duration
        Destroy(splatter, effectDuration);
    }
}
