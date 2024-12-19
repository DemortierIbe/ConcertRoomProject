using UnityEngine;
using UnityEngine.UI;

public class HandleYesNoButtons : MonoBehaviour
{
    public GameObject currentCanvas; 
    public GameObject yesCanvas;     
    public GameObject noCanvas;      

    public Button yesButton;         
    public Button noButton;          

    private void Start()
    {
        
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
