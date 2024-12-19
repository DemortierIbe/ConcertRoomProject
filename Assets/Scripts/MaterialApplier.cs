using UnityEngine;

public class MaterialApplier : MonoBehaviour
{
    [Header("Target Object Groups")]
    public GameObject[] firstTargetObjects; 
    public GameObject[] secondTargetObjects; 

    
    public void ApplyMaterialToFirstGroup(Material materialToApply)
    {
        ApplyMaterialToObjects(firstTargetObjects, materialToApply);
    }

    
    public void ApplyMaterialToSecondGroup(Material materialToApply)
    {
        ApplyMaterialToObjects(secondTargetObjects, materialToApply);
    }

    
    private void ApplyMaterialToObjects(GameObject[] targetObjects, Material materialToApply)
    {
        

        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = materialToApply; // Apply the material
                }
                
            }
        }

        Debug.Log($"Material {materialToApply.name} applied to target group.");
    }
}
