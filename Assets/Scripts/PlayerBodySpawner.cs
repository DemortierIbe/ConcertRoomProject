using UnityEngine;

public class PlayerBodySpawner : MonoBehaviour
{
    public Transform playerCamera;       
    public GameObject playerBodyInstance; 

    public float bodyHeightOffset = 1.75f; 

    void Start()
    {
        
        AlignBodyWithCamera();
    }

    void Update()
    {
        if (playerBodyInstance != null)
        {
            Vector3 bodyPosition = playerCamera.position;
            bodyPosition.y -= bodyHeightOffset; 

            playerBodyInstance.transform.position = bodyPosition;

            Vector3 bodyRotation = playerCamera.rotation.eulerAngles;
            bodyRotation.x = 0f;
            bodyRotation.z = 0f;
            playerBodyInstance.transform.rotation = Quaternion.Euler(bodyRotation);
        }
    }

    private void AlignBodyWithCamera()
    {
        
        Vector3 initialPosition = playerCamera.position;
        initialPosition.y -= bodyHeightOffset;

        playerBodyInstance.transform.position = initialPosition;

        
        Vector3 initialRotation = playerCamera.rotation.eulerAngles;
        initialRotation.x = 0f;
        initialRotation.z = 0f;

        playerBodyInstance.transform.rotation = Quaternion.Euler(initialRotation);

        Debug.Log("Player body aligned with the camera at start.");
    }
}
