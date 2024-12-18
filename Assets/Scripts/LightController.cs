using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    // Sliders for controlling rotation of target objects
    public Slider sliderX;
    public Slider sliderY;

    // Sliders for controlling rotation of lights
    public Slider lightSliderX;
    public Slider lightSliderY;

    // Sliders for controlling color of target materials
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    // Sliders for controlling color of light materials
    public Slider lightRedSlider;
    public Slider lightGreenSlider;
    public Slider lightBlueSlider;

    // Lists of target objects and lights
    public Transform[] targetObjects;  // List of target objects to rotate
    public Transform[] lights;         // List of lights to rotate

    // Toggle buttons for showing/hiding target objects and lights
    public Toggle showHideObjectsToggle;  // Toggle for target objects
    public Toggle showHideLightsToggle;   // Toggle for lights

    private Material[] targetMaterials;
    private Material[] lightMaterials;

    void Start()
    {
        // Initialize the materials for target objects and lights
        targetMaterials = new Material[targetObjects.Length];
        for (int i = 0; i < targetObjects.Length; i++)
        {
            targetMaterials[i] = GetChildMaterial(targetObjects[i]);
        }

        lightMaterials = new Material[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            lightMaterials[i] = GetChildMaterial(lights[i]);
        }

        // Set the toggle state to off initially (hide objects and lights)
        showHideObjectsToggle.isOn = false;
        showHideLightsToggle.isOn = false;

        // Hide objects and lights based on the initial toggle state
        OnObjectsToggleValueChanged(false);  // Hide target objects
        OnLightsToggleValueChanged(false);   // Hide lights

        // Add listeners to the toggles to handle state changes
        showHideObjectsToggle.onValueChanged.AddListener(OnObjectsToggleValueChanged);
        showHideLightsToggle.onValueChanged.AddListener(OnLightsToggleValueChanged);
    }


    void Update()
    {
        // Update rotation and color as usual
        Vector3 targetRotation = new Vector3(sliderX.value, sliderY.value, 0);
        foreach (Transform target in targetObjects)
        {
            target.rotation = Quaternion.Euler(targetRotation);
        }

        Vector3 lightRotation = new Vector3(lightSliderX.value, lightSliderY.value, 0);
        foreach (Transform light in lights)
        {
            light.rotation = Quaternion.Euler(lightRotation);
        }

        // Update materials for the target objects and lights
        UpdateMaterials(targetMaterials, redSlider.value, greenSlider.value, blueSlider.value);
        UpdateMaterials(lightMaterials, lightRedSlider.value, lightGreenSlider.value, lightBlueSlider.value);
    }

    private Material GetChildMaterial(Transform parent)
    {
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
                Color currentColor = mat.color;
                float alpha = currentColor.a;
                mat.color = new Color(red, green, blue, alpha);
            }
        }
    }

    // Method for hiding/showing target object children based on the toggle state
    private void OnObjectsToggleValueChanged(bool isToggled)
    {
        foreach (Transform target in targetObjects)
        {
            // Get the first child (the child that contains the material)
            if (target.childCount > 0)
            {
                Transform childLevel1 = target.GetChild(0);
                if (childLevel1.childCount > 0)
                {
                    Transform childLevel2 = childLevel1.GetChild(0); // The actual child with the material
                    childLevel2.gameObject.SetActive(isToggled);  // Show/hide only this specific child
                }
            }
        }
    }

    // Method for hiding/showing light children based on the toggle state
    private void OnLightsToggleValueChanged(bool isToggled)
    {
        foreach (Transform light in lights)
        {
            // Get the first child (the child that contains the material)
            if (light.childCount > 0)
            {
                Transform childLevel1 = light.GetChild(0);
                if (childLevel1.childCount > 0)
                {
                    Transform childLevel2 = childLevel1.GetChild(0); // The actual child with the material
                    childLevel2.gameObject.SetActive(isToggled);  // Show/hide only this specific child
                }
            }
        }
    }

}
