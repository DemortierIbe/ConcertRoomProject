using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro support

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSources; // List of AudioSources
    public List<AudioClip> audioClips;     // List of AudioClips for skipping
    public Slider volumeSlider;           // Slider for volume control
    public Slider pitchSlider;            // Slider for pitch control
    public Slider progressSlider;         // Slider for showing/controlling song progress
    public TextMeshProUGUI elapsedTimeText; // Text for elapsed time
    public TextMeshProUGUI totalTimeText;   // Text for total duration
    public GameObject clipButtonPrefab;   // Prefab for the UI button
    public Transform clipListParent;      // Parent object for the clip buttons

    private int currentClipIndex = 0;     // Index of the current AudioClip
    private bool isPlaying = false;       // Track the play/pause state

    void Start()
    {
        // Ensure all AudioSources are paused and no music starts automatically
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }

        // Populate the UI list with buttons for each audio clip
        PopulateClipList();

        // Set up progress slider
        if (progressSlider != null)
        {
            progressSlider.minValue = 0;
            progressSlider.maxValue = 1;
            progressSlider.value = 0;
        }
    }

    void Update()
    {
        // Update volume and pitch values for all AudioSources
        foreach (AudioSource source in audioSources)
        {
            source.volume = volumeSlider.value;
            source.pitch = pitchSlider.value;
        }

        // Automatically move to the next track if the current one has finished
        if (isPlaying && !audioSources[0].isPlaying) // Check the first audio source
        {
            SkipToNext();
        }

        // Update progress slider and time texts if playing
        if (isPlaying)
        {
            UpdateProgress();
        }
    }

    public void TogglePlayPause()
    {
        if (isPlaying)
        {
            PauseMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        if (audioClips.Count == 0) return;

        // Play the current clip on all audio sources
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.clip = audioClips[currentClipIndex];
                source.Play();
            }
        }
        isPlaying = true;

        // Set total duration text
        UpdateTotalTime();
    }

    public void PauseMusic()
    {
        foreach (AudioSource source in audioSources)
        {
            source.Pause();
        }
        isPlaying = false;
    }

    public void SkipToNext()
    {
        if (audioClips.Count == 0) return;

        // Increment the clip index and loop back to the start if at the end
        currentClipIndex = (currentClipIndex + 1) % audioClips.Count;
        PlayClip(currentClipIndex);
    }

    public void SkipToPrevious()
    {
        if (audioClips.Count == 0) return;

        // Decrement the clip index and loop to the end if at the beginning
        currentClipIndex = (currentClipIndex - 1 + audioClips.Count) % audioClips.Count;
        PlayClip(currentClipIndex);
    }

    public void PlayClipFromUI(int index)
    {
        currentClipIndex = index;
        PlayClip(currentClipIndex);
    }

    private void PlayClip(int index)
    {
        foreach (AudioSource source in audioSources)
        {
            source.Stop(); // Stop any currently playing audio
            source.clip = audioClips[index];
            source.Play();
        }
        isPlaying = true;

        // Reset progress slider
        if (progressSlider != null)
        {
            progressSlider.value = 0;
        }

        // Update total time text
        UpdateTotalTime();
    }

    private void PopulateClipList()
    {
        if (clipButtonPrefab == null || clipListParent == null)
        {
            Debug.LogError("clipButtonPrefab or clipListParent is not assigned in the Inspector.");
            return;
        }

        // Clear existing buttons if necessary
        foreach (Transform child in clipListParent)
        {
            Destroy(child.gameObject);
        }

        // Create a button for each audio clip
        for (int i = 0; i < audioClips.Count; i++)
        {
            int clipIndex = i; // Local copy for the button callback
            GameObject button = Instantiate(clipButtonPrefab, clipListParent);

            // Reset the button's RectTransform for proper layout
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            if (buttonRect != null)
            {
                buttonRect.localScale = Vector3.one; // Reset scale
                buttonRect.anchoredPosition3D = Vector3.zero; // Reset position
                buttonRect.localPosition = Vector3.zero; // Ensure localPosition is correct
            }

            // Set the button text
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = audioClips[i].name;
            }
            else
            {
                Debug.LogError("Button prefab is missing a TextMeshProUGUI component.");
            }

            // Add click listener to the button
            button.GetComponent<Button>().onClick.AddListener(() => PlayClipFromUI(clipIndex));
        }
    }

    private void UpdateProgress()
    {
        if (audioSources[0].clip != null)
        {
            // Update slider value based on the current playback time
            progressSlider.value = audioSources[0].time / audioSources[0].clip.length;

            // Update elapsed time text
            elapsedTimeText.text = FormatTime(audioSources[0].time);
        }
    }

    public void OnProgressSliderChanged()
    {
        if (audioSources[0].clip != null)
        {
            // Set playback time based on slider value
            audioSources[0].time = progressSlider.value * audioSources[0].clip.length;

            // Sync all audio sources
            foreach (AudioSource source in audioSources)
            {
                source.time = audioSources[0].time;
            }
        }
    }

    private void UpdateTotalTime()
    {
        if (audioSources[0].clip != null)
        {
            totalTimeText.text = FormatTime(audioSources[0].clip.length);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}