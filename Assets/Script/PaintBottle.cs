using UnityEngine;

public class PaintBottle : MonoBehaviour
{
    public Color paintColor;  // Color of the paint inside the bottle

    void Start()
    {
        // You can assign a random color, or set predefined colors
        paintColor = Random.ColorHSV();
    }
}
