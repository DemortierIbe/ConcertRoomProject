using UnityEngine;

public class InstantiateOnUIButton : MonoBehaviour
{
    public GameObject objectToInstantiate; // Prefab to instantiate
    public Transform spawnLocation;       // Location where the object should spawn

    // This method will be called when the UI button is pressed
    public void SpawnObject()
    {
        if (objectToInstantiate != null && spawnLocation != null)
        {
            // Instantiate the object at the specified location and rotation
            Instantiate(objectToInstantiate, spawnLocation.position, spawnLocation.rotation);
        }
        else
        {
            Debug.LogError("Object to instantiate or spawn location is not assigned.");
        }
    }
}
