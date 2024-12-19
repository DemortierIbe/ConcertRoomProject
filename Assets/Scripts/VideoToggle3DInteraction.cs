using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Video;

public class VideoToggle3DInteraction : MonoBehaviour
{
    public GameObject videoPlayerObject; // The 3D object containing the Video Player
    public VideoPlayer videoPlayer; // The Video Player component

    private bool isVideoPlaying = false; // Tracks video state

    private void Start()
    {
        // Ensure the video player is hidden and paused at the start
        if (videoPlayerObject != null)
            videoPlayerObject.SetActive(false);

        if (videoPlayer != null)
            videoPlayer.Pause();
    }

    // Called when the object is gripped (OnSelectEntered)
    public void OnSelectEntered()
    {
        // Toggle video player visibility and playback state
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
