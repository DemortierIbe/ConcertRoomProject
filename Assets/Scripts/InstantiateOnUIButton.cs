using UnityEngine;

public class InstantiateOnUIButton : MonoBehaviour
{
    public GameObject objectToInstantiate; 
    public Transform spawnLocation;       

    
    public void SpawnObject()
    {
        if (objectToInstantiate != null && spawnLocation != null)
        {
            
            Instantiate(objectToInstantiate, spawnLocation.position, spawnLocation.rotation);
        }
        
    }
}
