using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCanvasOnClick : MonoBehaviour
{
    public GameObject canvasToShow;    // The first canvas to toggle
    public GameObject canvasToHide;    // The second canvas to toggle
    public GameObject thirdCanvas;     // The third canvas to hide when gripping again

    private void Start()
    {
        // Ensure initial states of canvases
        if (canvasToShow != null)
            canvasToShow.SetActive(false); // Hide the first canvas initially

        if (canvasToHide != null)
            canvasToHide.SetActive(true);  // Show the second canvas initially

        if (thirdCanvas != null)
            thirdCanvas.SetActive(false);  // Ensure the third canvas is hidden initially
    }

    // Called when the object is gripped (OnSelectEntered)
    public void OnSelectEntered()
    {
        // Hide the third canvas first, if active
        if (thirdCanvas != null && thirdCanvas.activeSelf)
        {
            thirdCanvas.SetActive(false);
        }

        // Continue with the original toggle logic
        if (canvasToShow != null)
        {
            bool isActive = canvasToShow.activeSelf;
            canvasToShow.SetActive(!isActive); // Toggle the visibility of the first canvas

            if (canvasToHide != null)
            {
                canvasToHide.SetActive(isActive); // Show the second canvas when the first is hidden
            }
        }
    }
}
