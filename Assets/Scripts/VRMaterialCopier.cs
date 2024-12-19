using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMaterialCopier : MonoBehaviour
{
    
    public Material bodyMaterial;      
    public Material assAndLegsMaterial; 
    public GameObject playerModel;     

    private void Start()
    {
        
        if (playerModel == null)
        {
            playerModel = GameObject.Find("Playerbody");
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

        
        Transform bodyTransform = playerModel.transform.Find("Body");
        Transform leftLegTransform = playerModel.transform.Find("LeftLeg");
        Transform rightLegTransform = playerModel.transform.Find("RightLeg");
        Transform assTransform = playerModel.transform.Find("Ass");

        

        
        Renderer bodyRenderer = bodyTransform.GetComponentInChildren<Renderer>();
        Renderer leftLegRenderer = leftLegTransform.GetComponentInChildren<Renderer>();
        Renderer rightLegRenderer = rightLegTransform.GetComponentInChildren<Renderer>();
        Renderer assRenderer = assTransform.GetComponentInChildren<Renderer>();

        

        
        bodyRenderer.sharedMaterial = bodyMaterial;
        leftLegRenderer.sharedMaterial = assAndLegsMaterial;
        rightLegRenderer.sharedMaterial = assAndLegsMaterial;
        assRenderer.sharedMaterial = assAndLegsMaterial;

        Debug.Log("Applied materials to Playerbody's Body, LeftLeg, RightLeg, and Ass parts.");
    }
}
