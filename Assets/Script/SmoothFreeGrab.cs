using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class SmoothFreeGrab : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Transform lastAttachTransform;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Clean up any previous attach point
        if (lastAttachTransform != null)
        {
            Destroy(lastAttachTransform.gameObject);
        }

        // Create attach point relative to the object
        Transform interactorTransform = args.interactorObject.transform;

        // Compute local offset from object to interactor
        Vector3 localPos = transform.InverseTransformPoint(interactorTransform.position);
        Quaternion localRot = Quaternion.Inverse(transform.rotation) * interactorTransform.rotation;

        GameObject dynamicAttach = new GameObject("DynamicAttachPoint");
        dynamicAttach.transform.SetParent(transform);
        dynamicAttach.transform.localPosition = localPos;
        dynamicAttach.transform.localRotation = localRot;

        // Set as attach point
        grabInteractable.attachTransform = dynamicAttach.transform;
        lastAttachTransform = dynamicAttach.transform;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Remove the dynamic attach point
        if (lastAttachTransform != null)
        {
            Destroy(lastAttachTransform.gameObject);
            lastAttachTransform = null;
        }

        grabInteractable.attachTransform = null;
    }
}
