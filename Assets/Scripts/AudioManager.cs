using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    List<AudioSource> audioSources;

    void Start()
    {
        audioSources = new List<AudioSource>(GetComponents<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
