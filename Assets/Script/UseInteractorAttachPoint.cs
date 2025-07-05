using UnityEngine;


[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class UseInteractorAttachPoint : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Let the interactor provide the attach point automatically
        grabInteractable.trackPosition = true;
        grabInteractable.trackRotation = true;

        // Clear any custom attach transform — we want to use the interactor's attach point
        grabInteractable.attachTransform = null;

        // Optional: Zero out ease-in for snappier grabs
        grabInteractable.attachEaseInTime = 0f;

        // Make sure physics isn't interfering (especially for near-field interaction)
        grabInteractable.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.Kinematic;
    }
}
