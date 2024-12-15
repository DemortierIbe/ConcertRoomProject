using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCanvasOnClick : MonoBehaviour
{
    public GameObject canvasToShow; // The Canvas to toggle

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false); // Ensure the canvas is hidden initially
        }
    }

    // Called when the trigger button activates the object
    public void OnSelectEntered()
    {
        if (canvasToShow != null)
        {
            bool isActive = canvasToShow.activeSelf;
            canvasToShow.SetActive(!isActive); // Toggle the canvas visibility
        }
    }
}