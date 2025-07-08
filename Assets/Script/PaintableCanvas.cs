using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PaintableCanvas : MonoBehaviour
{
    public Texture2D paintTexture;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Create a 512x512 texture that we can draw to
        paintTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        paintTexture.filterMode = FilterMode.Point;
        paintTexture.wrapMode = TextureWrapMode.Clamp;

        // Fill it with white color
        Color[] fillColor = new Color[paintTexture.width * paintTexture.height];
        for (int i = 0; i < fillColor.Length; i++) fillColor[i] = Color.white;
        paintTexture.SetPixels(fillColor);
        paintTexture.Apply();

        // Assign the writable texture to the material
        rend.material.mainTexture = paintTexture;
    }

    // This method will be called by the roller
    public void PaintAtUV(Vector2 uv, Color color, int brushSize = 4)
    {
        int x = (int)(uv.x * paintTexture.width);
        int y = (int)(uv.y * paintTexture.height);

        for (int i = -brushSize; i < brushSize; i++)
        {
            for (int j = -brushSize; j < brushSize; j++)
            {
                int px = Mathf.Clamp(x + i, 0, paintTexture.width - 1);
                int py = Mathf.Clamp(y + j, 0, paintTexture.height - 1);
                paintTexture.SetPixel(px, py, color);
            }
        }

        paintTexture.Apply();
    }
}
