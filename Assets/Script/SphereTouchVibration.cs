using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class SphereTouchVibration : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    private bool isTouchingAnother = false;

    private Vector3 originalScale;
    private Renderer sphereRenderer;
    private Color originalColor;

    [Header("Visual Pulse Settings")]
    public float pulseSpeed = 5f;
    public float pulseAmount = 0.05f;
    private float pulseTimer = 0f;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);

        originalScale = transform.localScale;
        sphereRenderer = GetComponent<Renderer>();
        if (sphereRenderer != null)
            originalColor = sphereRenderer.material.color;
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    void Update()
    {
        if (isTouchingAnother && !isGrabbed)
        {
            pulseTimer += Time.deltaTime * pulseSpeed;
            float scaleFactor = 1 + Mathf.Sin(pulseTimer) * pulseAmount;
            transform.localScale = originalScale * scaleFactor;

            if (sphereRenderer != null)
                sphereRenderer.material.color = Color.Lerp(originalColor, Color.magenta, 0.5f);
        }
        else
        {
            // Reset scale and color
            transform.localScale = originalScale;
            if (sphereRenderer != null)
                sphereRenderer.material.color = originalColor;
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        isTouchingAnother = false;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isGrabbed) return;

        if (collision.gameObject != gameObject &&
            collision.gameObject.GetComponent<SphereTouchVibration>() != null)
        {
            isTouchingAnother = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<SphereTouchVibration>() != null)
        {
            isTouchingAnother = false;
        }
    }
}
