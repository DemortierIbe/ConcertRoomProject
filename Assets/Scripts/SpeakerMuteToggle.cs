using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpeakerAndObjectToggle : MonoBehaviour
{
    public GameObject[] speakers;       // Array of speaker GameObjects
    public GameObject[] toggleObjects; // Array of 3D objects to show/hide

    private bool isMuted = true;        // Tracks whether speakers are muted

    private void Start()
    {
        // Initialize by muting speakers and hiding objects
        foreach (GameObject speaker in speakers)
        {
            AudioSource audioSource = speaker.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.enabled = false; // Mute speakers
            }
            else
            {
                Debug.LogError($"Speaker {speaker.name} is missing an AudioSource component.");
            }
        }

        foreach (GameObject obj in toggleObjects)
        {
            obj.SetActive(false); // Hide toggle objects
        }
    }

    public void OnSelectEntered()
    {
        // Toggle the mute state of speakers
        isMuted = !isMuted;
        foreach (GameObject speaker in speakers)
        {
            AudioSource audioSource = speaker.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.enabled = !isMuted; // Toggle mute state
            }
        }

        // Toggle visibility of 3D objects
        foreach (GameObject obj in toggleObjects)
        {
            obj.SetActive(!obj.activeSelf); // Show/hide objects
        }

        Debug.Log(isMuted ? "Speakers muted and objects hidden." : "Speakers unmuted and objects shown.");
    }
}
