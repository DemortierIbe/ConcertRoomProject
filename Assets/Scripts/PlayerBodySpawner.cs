using UnityEngine;

public class PlayerBodySpawner : MonoBehaviour
{
    public Transform playerCamera;       // Reference to the player's camera (headset)
    public GameObject playerBodyInstance; // Reference to the player body already in the scene

    public float bodyHeightOffset = 1.75f; // Height from the ground to the head (adjust based on your prefab)

    void Start()
    {
        // Ensure required references are assigned
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned.");
            return;
        }

        if (playerBodyInstance == null)
        {
            Debug.LogError("Player Body Instance is not assigned.");
            return;
        }

        // Optionally adjust the initial position of the body to align with the camera
        AlignBodyWithCamera();
    }

    void Update()
    {
        if (playerBodyInstance != null)
        {
            // Update the body's position to follow the player
            Vector3 bodyPosition = playerCamera.position;
            bodyPosition.y -= bodyHeightOffset; // Adjust for body height to keep feet on the ground

            playerBodyInstance.transform.position = bodyPosition;

            // Update the body's rotation to match the camera's rotation on the Y-axis only
            Vector3 bodyRotation = playerCamera.rotation.eulerAngles;
            bodyRotation.x = 0f;
            bodyRotation.z = 0f;
            playerBodyInstance.transform.rotation = Quaternion.Euler(bodyRotation);
        }
    }

    private void AlignBodyWithCamera()
    {
        // Adjust the body position based on the camera's position and height offset
        Vector3 initialPosition = playerCamera.position;
        initialPosition.y -= bodyHeightOffset;

        playerBodyInstance.transform.position = initialPosition;

        // Align rotation to the camera's Y-axis rotation
        Vector3 initialRotation = playerCamera.rotation.eulerAngles;
        initialRotation.x = 0f;
        initialRotation.z = 0f;

        playerBodyInstance.transform.rotation = Quaternion.Euler(initialRotation);

        Debug.Log("Player body aligned with the camera at start.");
    }
}
