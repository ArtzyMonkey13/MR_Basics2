using UnityEngine;

public class PaintTray : MonoBehaviour
{
    public Color[] colorSequence = {
        new Color(1f, 0.4f, 0.7f), // Pink
        Color.red,
        new Color(1f, 0.55f, 0f),  // Orange
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(0.5f, 0f, 0.5f),     // Purple
        new Color(0.4f, 0.26f, 0.13f), // Brown
        Color.black,
        Color.gray,
        Color.white
    };

    private int currentColorIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        PaintRoller roller = other.GetComponent<PaintRoller>();
        if (roller != null)
        {
            Color nextColor = colorSequence[currentColorIndex];
            roller.SetPaintColor(nextColor);

            Debug.Log($"Changed roller to color: {nextColor}");

            currentColorIndex = (currentColorIndex + 1) % colorSequence.Length;
        }
    }
}
