using UnityEngine;

public class PaintPaletteZone : MonoBehaviour
{
    public Color zoneColor = Color.red;

    private void OnTriggerEnter(Collider other)
    {
        PaintBrush brush = other.GetComponent<PaintBrush>();
        if (brush != null)
        {
            brush.SetPaintColor(zoneColor);
            Debug.Log($"{gameObject.name}: Brush color set to {zoneColor}");
        }
    }
}
