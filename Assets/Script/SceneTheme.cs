using UnityEngine;

[System.Serializable]
public class SceneTheme
{
    public string themeName;

    [Header("Materials")]
    public Material wallMaterial;
    public Material floorMaterial;
    public Material furnitureMaterial;

    [Header("Sculpture Color Palette")]
    public Color[] sculptureColors;
}
