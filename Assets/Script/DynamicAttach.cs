using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicAttach : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Register to grabbing events
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Create a temporary attach point at the interactor's current position
        Transform interactorTransform = args.interactorObject.transform;

        GameObject dynamicAttach = new GameObject("DynamicAttach");
        dynamicAttach.transform.SetPositionAndRotation(interactorTransform.position, interactorTransform.rotation);
        dynamicAttach.transform.SetParent(transform);

        // Use that as the attach point
        grabInteractable.attachTransform = dynamicAttach.transform;
    }
}
