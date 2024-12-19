using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCanvasOnClick : MonoBehaviour
{
    public GameObject canvasToShow;    
    public GameObject canvasToHide;    
    public GameObject thirdCanvas;     

    private void Start()
    {
        if (canvasToShow != null)
            canvasToShow.SetActive(false); 

        if (canvasToHide != null)
            canvasToHide.SetActive(true);  

        if (thirdCanvas != null)
            thirdCanvas.SetActive(false);  
    }

    public void OnSelectEntered()
    {
        if (thirdCanvas != null && thirdCanvas.activeSelf)
        {
            thirdCanvas.SetActive(false);
        }

        if (canvasToShow != null)
        {
            bool isActive = canvasToShow.activeSelf;
            canvasToShow.SetActive(!isActive); 

            if (canvasToHide != null)
            {
                canvasToHide.SetActive(isActive); 
            }
        }
    }
}
