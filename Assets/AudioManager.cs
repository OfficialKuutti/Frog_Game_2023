using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public AudioSource[] audioSources;
    public float seconds = 25.658f;
   
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
        yield return new WaitForSeconds(seconds);
         audioSources[1].volume = 0.7f;
        //audioSources[1].Play();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (musicpoint1 == true)
        {
            audioSources[1].volume = Mathf.Lerp(audioSources[1].volume, 0f, 4f * Time.deltaTime);
            audioSources[2].volume = Mathf.Lerp(audioSources[2].volume, 0.7f, 2f * Time.deltaTime);
           
        }

        if (musicpoint2 == true)
        {
            musicpoint1 = false;
            audioSources[2].volume = Mathf.Lerp(audioSources[2].volume, 0f, 4f * Time.deltaTime);
            audioSources[3].volume = Mathf.Lerp(audioSources[3].volume, 0.5f, 2f * Time.deltaTime);
           
        }
    }

   
}
