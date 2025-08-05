using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AirbrushSpray : MonoBehaviour
{
    public XRController leftController;  // Left XR controller
    public XRController rightController; // Right XR controller
    public ParticleSystem sprayEffect;   // The spray effect to show when spraying paint
    public LayerMask canvasLayer;        // Layer that the canvas is on
    public Color currentColor = Color.white; // The current spray color
    public int brushSize = 5;           // Size of the spray brush
    public bool useRandomSplatter = true;  // Option for random splatter

    private bool isSpraying = false;
    private GameObject activeController;  // The active controller (based on dominant hand)

    public enum HandPreference
    {
        Left,
        Right
    }

    public HandPreference playerHandPreference = HandPreference.Right;  // Default to right hand

    private ParticleSystem.EmissionModule emissionModule;  // To control emission

    void Start()
    {
        // Set the active controller based on the dominant hand
        SetActiveController();

        // Get the emission module from the sprayEffect
        emissionModule = sprayEffect.emission;
        
        // Initially disable the particle system
        emissionModule.enabled = false;  
    }

    void Update()
    {
        // Check if the trigger button (squeeze) is pressed
        if (IsTriggerPressed() && !isSpraying)
        {
            isSpraying = true;
            sprayEffect.Play();  // Start the particle system (paint spray)
            emissionModule.enabled = true;  // Enable emission to start spraying
        }
        else if (!IsTriggerPressed() && isSpraying)
        {
            isSpraying = false;
            sprayEffect.Stop();  // Stop the particle system
            emissionModule.enabled = false;  // Disable emission when the trigger is released
        }

        if (isSpraying)
        {
            SprayPaint();  // Spray the paint when trigger is pressed
        }
    }

    // Check if the trigger on the active controller is pressed
    bool IsTriggerPressed()
    {
        // Get the input device from the active controller
        InputDevice controllerInputDevice = activeController.GetComponent<XRController>().inputDevice;

        bool triggerPressed = false;
        if (controllerInputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            return true;
        }
        return false;
    }

    void SprayPaint()
    {
        RaycastHit hit;

        // Use the active controller's position and direction for raycasting
        if (Physics.Raycast(activeController.transform.position, activeController.transform.forward, out hit, Mathf.Infinity, canvasLayer))
        {
            // Ensure that we hit an object with the PaintableCanvas script
            PaintableCanvas canvas = hit.collider.GetComponent<PaintableCanvas>();
            if (canvas != null)
            {
                // Convert the hit point to UV coordinates
                Vector2 uv = hit.textureCoord;

                // Paint on the canvas using the PaintableCanvas's PaintAtUV method
                canvas.PaintAtUV(uv, currentColor, brushSize, useRandomSplatter);
            }
        }
    }

    // Method to set the active controller based on the player's hand preference
    void SetActiveController()
    {
        // Based on the player's dominant hand preference, assign the active controller
        if (playerHandPreference == HandPreference.Left)
        {
            activeController = leftController.gameObject;  // Use the left controller GameObject
        }
        else
        {
            activeController = rightController.gameObject;  // Use the right controller GameObject
        }
    }
}
