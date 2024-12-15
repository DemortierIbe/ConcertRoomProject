using UnityEngine;
using UnityEngine.UI;

public class RotationAndLightController : MonoBehaviour
{
    // Sliders for controlling rotation of target objects
    public Slider sliderX; // Slider to control X-axis rotation for target objects
    public Slider sliderY; // Slider to control Y-axis rotation for target objects

    // Sliders for controlling rotation of lights
    public Slider lightSliderX; // Slider to control X-axis rotation for lights
    public Slider lightSliderY; // Slider to control Y-axis rotation for lights

    // Sliders for controlling color of target materials
    public Slider redSlider;   // Red slider for target objects
    public Slider greenSlider; // Green slider for target objects
    public Slider blueSlider;  // Blue slider for target objects

    // Sliders for controlling color of light materials
    public Slider lightRedSlider;   // Red slider for light materials
    public Slider lightGreenSlider; // Green slider for light materials
    public Slider lightBlueSlider;  // Blue slider for light materials

    // Lists of target objects and lights
    public Transform[] targetObjects; // Array of target objects to rotate
    public Transform[] lights;        // Array of lights to rotate

    // Store the materials for each child object of target objects
    private Material[] targetMaterials;
    private Material[] lightMaterials;

    void Start()
    {
        // Initialize the materials array for target objects
        targetMaterials = new Material[targetObjects.Length];
        for (int i = 0; i < targetObjects.Length; i++)
        {
            targetMaterials[i] = GetChildMaterial(targetObjects[i]);
        }

        // Initialize the materials array for lights
        lightMaterials = new Material[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            lightMaterials[i] = GetChildMaterial(lights[i]);
        }
    }

    void Update()
    {
        // Update target object rotation based on the slider values
        Vector3 targetRotation = new Vector3(sliderX.value, sliderY.value, 0); // Fixed Z rotation
        foreach (Transform target in targetObjects)
        {
            target.rotation = Quaternion.Euler(targetRotation);
        }

        // Update light rotation based on the light sliders
        Vector3 lightRotation = new Vector3(lightSliderX.value, lightSliderY.value, 0); // Fixed Z rotation
        foreach (Transform light in lights)
        {
            light.rotation = Quaternion.Euler(lightRotation);
        }

        // Update the material color of each target object's child based on the sliders
        UpdateMaterials(targetMaterials, redSlider.value, greenSlider.value, blueSlider.value);

        // Update the material color of each light's child based on the sliders
        UpdateMaterials(lightMaterials, lightRedSlider.value, lightGreenSlider.value, lightBlueSlider.value);
    }

    private Material GetChildMaterial(Transform parent)
    {
        // Validate hierarchy and return the material if present
        if (parent.childCount >= 1)
        {
            Transform childLevel1 = parent.GetChild(0);
            if (childLevel1.childCount >= 1)
            {
                Transform childLevel2 = childLevel1.GetChild(0);
                Renderer renderer = childLevel2.GetComponent<Renderer>();
                if (renderer != null)
                {
                    return renderer.material;
                }
            }
        }
        Debug.LogError($"Material not found in hierarchy of {parent.name}. Please check the setup.");
        return null;
    }

    private void UpdateMaterials(Material[] materials, float red, float green, float blue)
    {
        foreach (Material mat in materials)
        {
            if (mat != null)
            {
                // Get the current color and retain its alpha value
                Color currentColor = mat.color;
                float alpha = currentColor.a;

                // Set the new color with the sliders, preserving alpha
                mat.color = new Color(red, green, blue, alpha);
            }
        }
    }
}