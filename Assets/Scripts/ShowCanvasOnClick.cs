using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCanvasOnClick : MonoBehaviour
{
    public GameObject canvasToShow; // The Canvas to toggle
    public GameObject canvasToHide; // The additional Canvas to hide

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false); // Ensure the canvas is hidden initially
        }
        if (canvasToHide != null)
        {
            canvasToHide.SetActive(true); // Ensure the other canvas is shown initially
        }
    }

    // Called when the trigger button activates the object
    public void OnSelectEntered()
    {
        if (canvasToShow != null)
        {
            bool isActive = canvasToShow.activeSelf;
            canvasToShow.SetActive(!isActive); // Toggle the visibility of the first canvas
        }

        if (canvasToHide != null && canvasToHide.activeSelf)
        {
            canvasToHide.SetActive(false); // Hide the second canvas
        }
    }
}
