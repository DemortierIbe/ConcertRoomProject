using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour
{
    public GameObject canvasToHide;  
    public Button closeButton;       

    private void Start()
    {
        if (canvasToHide != null)
        {
            canvasToHide.SetActive(true);
        }

        if (closeButton != null)    //listener toevoegen
        {
            closeButton.onClick.AddListener(HideCanvas);
        }
        
    }

    private void HideCanvas()   //verberg canvas method
    {
        if (canvasToHide != null)
        {
            canvasToHide.SetActive(false);
        }
    }
}
