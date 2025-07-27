using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PaintableCanvas : MonoBehaviour
{
    public Texture2D paintTexture;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Create a writable canvas texture
        paintTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        paintTexture.filterMode = FilterMode.Point;
        paintTexture.wrapMode = TextureWrapMode.Clamp;

        // Fill with white initially
        Color[] fillColor = new Color[paintTexture.width * paintTexture.height];
        for (int i = 0; i < fillColor.Length; i++) fillColor[i] = Color.white;
        paintTexture.SetPixels(fillColor);
        paintTexture.Apply();

        rend.material.mainTexture = paintTexture;
    }

    public void PaintAtUV(Vector2 uv, Color color, int brushSize, bool useRandomSplatter = false)
    {
        int x = (int)(uv.x * paintTexture.width);
        int y = (int)(uv.y * paintTexture.height);

        for (int i = -brushSize; i < brushSize; i++)
        {
            for (int j = -brushSize; j < brushSize; j++)
            {
                // Optional randomness: only draw some of the pixels
                if (useRandomSplatter && Random.value > 0.6f) continue;

                // Slight jitter for more irregular splatter
                int offsetX = useRandomSplatter ? Random.Range(-1, 2) : 0;
                int offsetY = useRandomSplatter ? Random.Range(-1, 2) : 0;

                int px = Mathf.Clamp(x + i + offsetX, 0, paintTexture.width - 1);
                int py = Mathf.Clamp(y + j + offsetY, 0, paintTexture.height - 1);

                paintTexture.SetPixel(px, py, color);
            }
        }

        paintTexture.Apply();
    }
}
