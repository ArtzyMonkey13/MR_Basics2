using UnityEngine;

public class SceneStyler : MonoBehaviour
{
    [Header("Available Themes")]
    public SceneTheme[] themes;

    [Header("Scene Objects")]
    public Renderer[] walls;
    public Renderer floor;
    public Renderer[] furniture; // counters, tables, pedestals
    public Renderer[] sculptures;

    private SceneTheme currentTheme;

    void Start()
    {
        ApplyRandomTheme();
    }

    void ApplyRandomTheme()
    {
        currentTheme = themes[Random.Range(0, themes.Length)];
        Debug.Log("Applying theme: " + currentTheme.themeName);

        // Apply wall materials
        foreach (var wall in walls)
        {
            wall.material = currentTheme.wallMaterial;
        }

        // Apply floor material
        if (floor != null)
            floor.material = currentTheme.floorMaterial;

        // Apply furniture material
        foreach (var item in furniture)
        {
            item.material = currentTheme.furnitureMaterial;
        }

        // Apply sculpture colors
        foreach (var sculpture in sculptures)
        {
            Color color = currentTheme.sculptureColors[Random.Range(0, currentTheme.sculptureColors.Length)];

            if (sculpture.material.HasProperty("_BaseColor"))
                sculpture.material.SetColor("_BaseColor", color);
            else if (sculpture.material.HasProperty("_Color"))
                sculpture.material.color = color;
        }
    }
}
