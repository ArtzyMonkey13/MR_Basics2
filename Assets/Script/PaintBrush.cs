using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    [Header("Brush Settings")]
    public Transform brushTip;                // Bristle tip position
    public Renderer brushRenderer;            // Renderer for visual color
    public Color paintColor = Color.red;      // Current selected paint color
    public float rayDistance = 0.1f;          // How far to check for canvas
    public int brushSize = 4;                 // Standard brush stroke size

    [Header("Flick Splatter Settings")]
    public Rigidbody brushRigidbody;          // Rigidbody to measure motion
    public float flickVelocityThreshold = 1.5f; // Speed threshold to trigger splatter
    public float splatterSizeMultiplier = 3f;   // Scale for splatter brush size

    void Update()
    {
        if (brushTip == null) return;

        Vector3 origin = brushTip.position;
        Vector3 dir = -brushTip.up;

        Debug.DrawRay(origin, dir * rayDistance, Color.magenta);

        if (Physics.Raycast(origin, dir, out RaycastHit hit, rayDistance))
        {
            var canvas = hit.collider.GetComponent<PaintableCanvas>();
            if (canvas)
            {
                bool isFlicking = brushRigidbody != null &&
                                  brushRigidbody.linearVelocity.magnitude > flickVelocityThreshold;

                if (isFlicking)
                {
                    Debug.Log("💥 Flick detected! Applying splatter.");
                    int splatterSize = Mathf.RoundToInt(brushSize * splatterSizeMultiplier);
                    canvas.PaintAtUV(hit.textureCoord, paintColor, splatterSize, useRandomSplatter: true);
                }
                else
                {
                    canvas.PaintAtUV(hit.textureCoord, paintColor, brushSize);
                }
            }
        }
    }

    /// <summary>
    /// Change the paint color and update the visual brush tip
    /// </summary>
    /// <param name="newColor">Color to change to</param>
    public void SetPaintColor(Color newColor)
    {
        paintColor = newColor;
        UpdateBrushVisual();
        Debug.Log($"🎨 Paint color changed to: {newColor}");
    }

    /// <summary>
    /// Updates the brush tip material to match the current paint color
    /// </summary>
    private void UpdateBrushVisual()
    {
        if (brushRenderer != null)
        {
            brushRenderer.material.color = paintColor;
        }
    }
}
