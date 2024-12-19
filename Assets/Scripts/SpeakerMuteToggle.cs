using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpeakerMuteToggle : MonoBehaviour
{
    public GameObject[] speakers;       
    public GameObject[] toggleObjects;

    private bool isMuted = true;       


    private void Start()
    {
        foreach (GameObject speaker in speakers)
        {
            AudioSource audioSource = speaker.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.enabled = false; 
            }
            
        }

        foreach (GameObject obj in toggleObjects)
        {
            obj.SetActive(false); 
        }
    }

    public void OnSelectEntered()
    {
        
        isMuted = !isMuted;
        foreach (GameObject speaker in speakers)
        {
            AudioSource audioSource = speaker.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.enabled = !isMuted; 
            }
        }

     
        foreach (GameObject obj in toggleObjects)
        {
            obj.SetActive(!obj.activeSelf); 
        }

        Debug.Log(isMuted ? "Speakers muted and objects hidden." : "Speakers unmuted and objects shown.");
    }
}
