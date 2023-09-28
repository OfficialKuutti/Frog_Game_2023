using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public AudioSource[] audioSources;

   
    public bool musicpoint1 = false;
    public bool musicpoint2 = false;
        
    // Start is called before the first frame update
    void Start()
    {
        audioSources[0].Play();
        audioSources[1].volume = 0f;
        audioSources[2].volume = 0f;
        audioSources[3].volume = 0f;
        StartCoroutine("PlayNextTrack");
    }

    public IEnumerator PlayNextTrack()
    {
        yield return new WaitForSeconds(25.846f);
        audioSources[1].volume = 1f;
        audioSources[1].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSources[0].isPlaying)
        {
            audioSources[1].Play();
        }

        if (musicpoint1 == true)
        {
            audioSources[1].volume = Mathf.Lerp(audioSources[1].volume, 0f, 1f * Time.deltaTime);
            audioSources[2].volume = Mathf.Lerp(audioSources[2].volume, 1f, 1f * Time.deltaTime);
        }

        if (musicpoint2 == true)
        {
            audioSources[2].volume = Mathf.Lerp(audioSources[2].volume, 0f, 1f * Time.deltaTime);
            audioSources[3].volume = Mathf.Lerp(audioSources[3].volume, 1f, 1f * Time.deltaTime);
        }
    }

   
}
