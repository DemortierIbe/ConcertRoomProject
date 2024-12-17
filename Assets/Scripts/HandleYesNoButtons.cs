using UnityEngine;
using UnityEngine.UI;

public class HandleYesNoButtons : MonoBehaviour
{
    public GameObject currentCanvas; // The canvas with the Yes and No buttons
    public GameObject yesCanvas;     // The canvas to show when Yes is clicked
    public GameObject noCanvas;      // The canvas to show when No is clicked

    public Button yesButton;         // Reference to the Yes button
    public Button noButton;          // Reference to the No button

    private void Start()
    {
        // Ensure buttons are properly assigned and add listeners
        if (yesButton != null)
        {
            yesButton.onClick.AddListener(OnYesClicked);
        }

        if (noButton != null)
        {
            noButton.onClick.AddListener(OnNoClicked);
        }
    }

    private void OnYesClicked()
    {
        // Hide the current canvas and show the Yes canvas
        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
        }

        if (yesCanvas != null)
        {
            yesCanvas.SetActive(true);
        }
    }

    private void OnNoClicked()
    {
        // Hide the current canvas and show the No canvas
        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
        }

        if (noCanvas != null)
        {
            noCanvas.SetActive(true);
        }
    }
}
