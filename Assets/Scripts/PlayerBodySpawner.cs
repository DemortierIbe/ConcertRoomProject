using UnityEngine;

public class PlayerBodySpawner : MonoBehaviour
{
    public Transform playerCamera;       // Reference to the player's camera (headset)
    public GameObject playerBodyPrefab;  // The player body prefab (e.g., spheres, capsules, etc.)
    private GameObject playerBodyInstance; // Instance of the player body

    public float bodyHeightOffset = 1.75f; // Height from the ground to the head (adjust based on your prefab)

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned.");
            return;
        }

        if (playerBodyPrefab != null)
        {
            // Spawn the body at the correct position
            Vector3 spawnPosition = new Vector3(
                playerCamera.position.x,
                playerCamera.position.y - bodyHeightOffset, // Lower the body to align feet with the ground
                playerCamera.position.z
            );

            // Instantiate the body at the corrected position
            playerBodyInstance = Instantiate(playerBodyPrefab, spawnPosition, Quaternion.identity);

            Debug.Log("Player body spawned at " + spawnPosition);
        }
        else
        {
            Debug.LogError("Player Body Prefab is not assigned.");
        }
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
}
