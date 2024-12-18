using UnityEngine;

public class MaterialApplier : MonoBehaviour
{
    [Header("Target Object Groups")]
    public GameObject[] firstTargetObjects; // First group of objects
    public GameObject[] secondTargetObjects; // Second group of objects

    // Method to apply material to the first group of objects
    public void ApplyMaterialToFirstGroup(Material materialToApply)
    {
        ApplyMaterialToObjects(firstTargetObjects, materialToApply);
    }

    // Method to apply material to the second group of objects
    public void ApplyMaterialToSecondGroup(Material materialToApply)
    {
        ApplyMaterialToObjects(secondTargetObjects, materialToApply);
    }

    // General method to apply material to a list of objects
    private void ApplyMaterialToObjects(GameObject[] targetObjects, Material materialToApply)
    {
        if (materialToApply == null)
        {
            Debug.LogError("No material provided to apply!");
            return;
        }

        if (targetObjects == null || targetObjects.Length == 0)
        {
            Debug.LogError("No target objects assigned!");
            return;
        }

        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = materialToApply; // Apply the material
                }
                else
                {
                    Debug.LogWarning($"No Renderer found on {obj.name}, material not applied.");
                }
            }
        }

        Debug.Log($"Material {materialToApply.name} applied to target group.");
    }
}
