using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSources; // List AudioSources
    public List<AudioClip> audioClips;     // List AudioClips
    public Slider volumeSlider;           
    public Slider pitchSlider;            
    public Slider progressSlider;         
    public TextMeshProUGUI elapsedTimeText; 
    public TextMeshProUGUI totalTimeText;   
    public GameObject clipButtonPrefab;   //button voor list songs
    public Transform clipListParent;      

    private int currentClipIndex = 0;     
    private bool isPlaying = false;       

    void Start()
    {
        foreach (AudioSource source in audioSources)
        {
            source.Stop();      //pause all songs
        }

        PopulateClipList();

        if (progressSlider != null)
        {
            progressSlider.minValue = 0;
            progressSlider.maxValue = 1;
            progressSlider.value = 0;
        }
    }

    void Update()
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume = volumeSlider.value;
            source.pitch = pitchSlider.value;
        }

        if (isPlaying && !audioSources[0].isPlaying) // kijken of de audio nog speelt
        {
            SkipToNext();
        }

        
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

        
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.clip = audioClips[currentClipIndex];
                source.Play();
            }
        }
        isPlaying = true;

        
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

        
        currentClipIndex = (currentClipIndex + 1) % audioClips.Count;   //index++ als einde van de list terug naar begin
        PlayClip(currentClipIndex);
    }

    public void SkipToPrevious()
    {
        if (audioClips.Count == 0) return;

        
        currentClipIndex = (currentClipIndex - 1 + audioClips.Count) % audioClips.Count;        //index++ begin list naar einde van list
        PlayClip(currentClipIndex);
    }

    public void PlayClipFromUI(int index)
    {
        currentClipIndex = index;
        PlayClip(currentClipIndex);
    }

    private void PlayClip(int index)    //om te wisselen van liedje vorig stoppen en nieuw starten
    {
        foreach (AudioSource source in audioSources)
        {
            source.Stop(); 
            source.clip = audioClips[index];
            source.Play();
        }
        isPlaying = true;

        
        if (progressSlider != null)
        {
            progressSlider.value = 0;
        }

        
        UpdateTotalTime();
    }

    private void PopulateClipList()
    {
        if (clipButtonPrefab == null || clipListParent == null)
        {
            Debug.LogError("clipButtonPrefab or clipListParent is not assigned in the Inspector.");
            return;
        }

        
        foreach (Transform child in clipListParent)
        {
            Destroy(child.gameObject);
        }

        
        for (int i = 0; i < audioClips.Count; i++)
        {
            int clipIndex = i; 
            GameObject button = Instantiate(clipButtonPrefab, clipListParent);

            
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            if (buttonRect != null)
            {
                buttonRect.localScale = Vector3.one; 
                buttonRect.anchoredPosition3D = Vector3.zero; 
                buttonRect.localPosition = Vector3.zero; 
            }

            
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = audioClips[i].name;
            }
            else
            {
                Debug.LogError("Button prefab is missing a TextMeshProUGUI component.");
            }

            
            button.GetComponent<Button>().onClick.AddListener(() => PlayClipFromUI(clipIndex));
        }
    }

    private void UpdateProgress()
    {
        if (audioSources[0].clip != null)
        {
            
            progressSlider.value = audioSources[0].time / audioSources[0].clip.length;// slider value naar huide tijd

            
            elapsedTimeText.text = FormatTime(audioSources[0].time);
        }
    }

    public void OnProgressSliderChanged()
    {
        if (audioSources[0].clip != null)
        {
            
            audioSources[0].time = progressSlider.value * audioSources[0].clip.length;  //tijd lied aanpassen op basis sldier

            
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
            totalTimeText.text = FormatTime(audioSources[0].clip.length);   //totale tijd lied nemen
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}