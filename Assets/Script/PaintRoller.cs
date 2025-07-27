using UnityEngine;

public class PaintRoller : MonoBehaviour
{
    public Transform rollerTip;
    public Renderer rollerRenderer; // Assign this in the Inspector
    public Color paintColor = Color.red;
    public float rayDistance = 0.1f;
    public int brushSize = 4;

    void Start()
    {
        UpdateVisualColor();
    }

    void Update()
    {
        if (rollerTip == null)
        {
            Debug.LogWarning("RollerTip not assigned!");
            return;
        }

        Vector3 origin = rollerTip.position;
        Vector3 dir = -rollerTip.up;

        Debug.DrawRay(origin, dir * rayDistance, Color.green);

        if (Physics.Raycast(origin, dir, out RaycastHit hit, rayDistance))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            var canvas = hit.collider.GetComponent<PaintableCanvas>();
            if (canvas)
            {
                Debug.Log($"Painting at UV {hit.textureCoord}");
                canvas.PaintAtUV(hit.textureCoord, paintColor, brushSize);
            }
            else
            {
                Debug.Log("Hit something, but no PaintableCanvas found.");
            }
        }
    }

    public void SetPaintColor(Color newColor)
    {
        paintColor = newColor;
        UpdateVisualColor();
    }

    private void UpdateVisualColor()
    {
        if (rollerRenderer != null)
        {
            rollerRenderer.material.color = paintColor;
        }
    }
}
