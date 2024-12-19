using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    
    public Slider sliderX;
    public Slider sliderY;

    
    public Slider lightSliderX;
    public Slider lightSliderY;

    
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    
    public Slider lightRedSlider;
    public Slider lightGreenSlider;
    public Slider lightBlueSlider;

    
    public Transform[] targetObjects;  
    public Transform[] lights;         

    
    public Toggle showHideObjectsToggle;  
    public Toggle showHideLightsToggle;   

    private Material[] targetMaterials;
    private Material[] lightMaterials;

    void Start()
    {
        
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

        
        showHideObjectsToggle.isOn = false;     //toggle uit
        showHideLightsToggle.isOn = false;

        
        OnObjectsToggleValueChanged(false);  
        OnLightsToggleValueChanged(false);   

        
        showHideObjectsToggle.onValueChanged.AddListener(OnObjectsToggleValueChanged);
        showHideLightsToggle.onValueChanged.AddListener(OnLightsToggleValueChanged);
    }


    void Update()
    {
        
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

    
    private void OnObjectsToggleValueChanged(bool isToggled)        //aan uit zetten lampen op toggle
    {
        foreach (Transform target in targetObjects)
        {
            
            if (target.childCount > 0)
            {
                Transform childLevel1 = target.GetChild(0);
                if (childLevel1.childCount > 0)
                {
                    Transform childLevel2 = childLevel1.GetChild(0); 
                    childLevel2.gameObject.SetActive(isToggled);  
                }
            }
        }
    }

    
    private void OnLightsToggleValueChanged(bool isToggled)         //aan uit zetten lampen op toggle
    {
        foreach (Transform light in lights)
        {
            
            if (light.childCount > 0)
            {
                Transform childLevel1 = light.GetChild(0);
                if (childLevel1.childCount > 0)
                {
                    Transform childLevel2 = childLevel1.GetChild(0); 
                    childLevel2.gameObject.SetActive(isToggled);  
                }
            }
        }
    }

}
