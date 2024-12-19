using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour
{
    public GameObject canvasToHide;  // The Canvas you want to hide
    public Button closeButton;       // The button to close the canvas

    private void Start()
    {
        // Ensure the canvas is visible at the start
        if (canvasToHide != null)
        {
            canvasToHide.SetActive(true);
        }

        // Add listener to the button
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideCanvas);
        }
        else
        {
            Debug.LogError("Close button is not assigned.");
        }
    }

    // Method to hide the canvas
    private void HideCanvas()
    {
        if (canvasToHide != null)
        {
            canvasToHide.SetActive(false);
        }
    }
}
