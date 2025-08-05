using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableObject : MonoBehaviour
{
    public Transform attachTransform; // The attach transform location (empty GameObject)
    public Vector3 rotationOffset = new Vector3(0, 0, 0); // Optional offset for the rotation
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Subscribe to the event using the new API
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        // Get the controller position and rotation
        Transform controllerTransform = args.interactorObject.transform;

        // Calculate the rotation needed for the object to face forward
        // The object's rotation is now adjusted with an offset
        Quaternion desiredRotation = controllerTransform.rotation;

        // Optionally, apply a custom offset to the rotation to make it point forward
        // Here, we rotate the object slightly to ensure it's not pointing straight up
        desiredRotation *= Quaternion.Euler(rotationOffset);  // Apply rotation offset

        // Apply the calculated rotation to the object
        transform.rotation = desiredRotation;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }
}
