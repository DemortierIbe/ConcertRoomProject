using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Video;

public class VideoToggle3DInteraction : MonoBehaviour
{
    public GameObject videoPlayerObject; 
    public VideoPlayer videoPlayer; 

    private bool isVideoPlaying = false;

    private void Start()
    {
                                                //pauseren bij start
        if (videoPlayerObject != null)
            videoPlayerObject.SetActive(false);

        if (videoPlayer != null)
            videoPlayer.Pause();
    }

    
    public void OnSelectEntered()
    {
        
        isVideoPlaying = !isVideoPlaying;

        if (videoPlayerObject != null)
            videoPlayerObject.SetActive(isVideoPlaying);

        if (videoPlayer != null)
        {
            if (isVideoPlaying)
                videoPlayer.Play();
            else
                videoPlayer.Pause();
        }
    }
}
