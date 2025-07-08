using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class SphereXRGrabInteractable : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor currentInteractor;
    private bool isGrabbed = false;
    private float vibrationCheckInterval = 0.2f;
    private float vibrationRadius = 0.5f;
    private float nextCheckTime = 0f;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    void Update()
    {
        if (!isGrabbed && Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + vibrationCheckInterval;

            if (IsNearOtherSphere(out UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor))
            {
                SendHaptics(interactor);
            }
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        currentInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor;
        Debug.Log($"{gameObject.name} grabbed.");
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
        currentInteractor = null;
        Debug.Log($"{gameObject.name} released.");
    }

    private bool IsNearOtherSphere(out UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        interactor = null;

        Collider[] hits = Physics.OverlapSphere(transform.position, vibrationRadius);
        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject) continue; // Skip self

            var otherSphere = hit.GetComponent<SphereXRGrabInteractable>();
            if (otherSphere != null && !otherSphere.isGrabbed)
            {
                // Use other sphere's interactor if available
                interactor = otherSphere.currentInteractor;
                return true;
            }
        }

        return false;
    }

    private void SendHaptics(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        if (interactor == null) return;

        if (interactor is UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor directInteractor)
        {
            directInteractor.SendHapticImpulse(0.3f, 0.1f);
        }
    }

    // Optional: Draw gizmo for debug radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, vibrationRadius);
    }
}
