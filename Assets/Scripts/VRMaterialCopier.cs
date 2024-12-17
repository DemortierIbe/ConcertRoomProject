using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeMaterialOnInteract : MonoBehaviour
{
    // Public fields to assign materials for the body and shared ass/legs
    public Material bodyMaterial;      // Material for the body part
    public Material assAndLegsMaterial; // Shared material for ass, left leg, and right leg
    public GameObject playerModel;     // Reference to the PlayerBody (Player Model) that you want to change materials on

    private void Start()
    {
        // Ensure playerModel is assigned in the Inspector or automatically find it in the scene
        if (playerModel == null)
        {
            playerModel = GameObject.Find("Playerbody"); // Make sure to match the name of your PlayerBody GameObject
        }

        if (playerModel == null)
        {
            Debug.LogError("PlayerModel (Playerbody) not found in the scene!");
            return;
        }
    }

    public void ChangeMaterial()
    {
        if (playerModel == null)
        {
            Debug.LogError("PlayerModel (Playerbody) is not assigned or not found in the scene!");
            return;
        }

        // Find the parts of the Playerbody where the materials need to be copied
        Transform bodyTransform = playerModel.transform.Find("Body");
        Transform leftLegTransform = playerModel.transform.Find("LeftLeg");
        Transform rightLegTransform = playerModel.transform.Find("RightLeg");
        Transform assTransform = playerModel.transform.Find("Ass");

        if (bodyTransform == null || leftLegTransform == null || rightLegTransform == null || assTransform == null)
        {
            Debug.LogError("One or more of the required parts (Body, LeftLeg, RightLeg, Ass) not found in Playerbody!");
            return;
        }

        // Get the renderers for each part
        Renderer bodyRenderer = bodyTransform.GetComponentInChildren<Renderer>();
        Renderer leftLegRenderer = leftLegTransform.GetComponentInChildren<Renderer>();
        Renderer rightLegRenderer = rightLegTransform.GetComponentInChildren<Renderer>();
        Renderer assRenderer = assTransform.GetComponentInChildren<Renderer>();

        if (bodyRenderer == null || leftLegRenderer == null || rightLegRenderer == null || assRenderer == null)
        {
            Debug.LogWarning("One or more parts (Body, LeftLeg, RightLeg, Ass) are missing Renderers in Playerbody!");
            return;
        }

        // Apply the selected materials
        bodyRenderer.sharedMaterial = bodyMaterial;
        leftLegRenderer.sharedMaterial = assAndLegsMaterial;
        rightLegRenderer.sharedMaterial = assAndLegsMaterial;
        assRenderer.sharedMaterial = assAndLegsMaterial;

        Debug.Log("Applied materials to Playerbody's Body, LeftLeg, RightLeg, and Ass parts.");
    }
}
