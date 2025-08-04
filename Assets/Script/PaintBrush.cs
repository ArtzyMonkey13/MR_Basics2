using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    [Header("Brush Settings")]
    public Transform brushTip;
    public Renderer brushRenderer;
    public Color paintColor = Color.red;
    public float rayDistance = 0.1f;
    public int brushSize = 4;

    [Header("Flick Splatter Settings")]
    public Rigidbody brushRigidbody;
    public float flickVelocityThreshold = 1.5f;
    public float splatterSizeMultiplier = 3f;
    public float flickRayDistance = 1.5f; // 🎯 New: splatter from a distance

    void Update()
    {
        if (brushTip == null) return;

        Vector3 origin = brushTip.position;
        Vector3 dir = -brushTip.up;

        bool isFlicking = brushRigidbody != null &&
                          brushRigidbody.linearVelocity.magnitude > flickVelocityThreshold;

        float activeRayDistance = isFlicking ? flickRayDistance : rayDistance;

        Debug.DrawRay(origin, dir * activeRayDistance, isFlicking ? Color.red : Color.magenta);

        if (Physics.Raycast(origin, dir, out RaycastHit hit, activeRayDistance))
        {
            var canvas = hit.collider.GetComponent<PaintableCanvas>();
            if (canvas)
            {
                if (isFlicking)
                {
                    Debug.Log("💥 Distance flick splatter!");
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

    public void SetPaintColor(Color newColor)
    {
        paintColor = newColor;
        UpdateBrushVisual();
        Debug.Log($"🎨 Paint color changed to: {newColor}");
    }

    private void UpdateBrushVisual()
    {
        if (brushRenderer != null)
        {
            brushRenderer.material.color = paintColor;
        }
    }
}
