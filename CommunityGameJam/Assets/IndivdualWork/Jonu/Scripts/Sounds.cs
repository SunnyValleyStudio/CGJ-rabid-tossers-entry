using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public float chance;
}

[RequireComponent(typeof(AudioSource))]
public class Sounds : MonoBehaviour
{
    // volume will be randomly set to between those two
    public float volumeMin;
    public float volumeMax;

    // sounds
    public Sound[] sounds;


    // number of clips
    int size;
    // all the chances combined
    float chanceAll;
    // current chosen clip
    AudioClip chosenClip;
    // current audio source
    AudioSource current;
    float currentTime = 0;
    public float playSoundDelayMin = 3f;
    public float playSoundDelayMax = 5f;
    float playSoundDelay = 3f;
    void Start()
    {
        playSoundDelay = Random.Range(playSoundDelayMin, playSoundDelayMax);
    }

    
    void Update()
    {
        
        if (Static.paused == false)
        {
            currentTime += Time.deltaTime;
            if (currentTime > playSoundDelay)
            {
                Play();
                currentTime = 0;
                playSoundDelay = Random.Range(playSoundDelayMin, playSoundDelayMax);
            }
        }
    }

    // play a random sound
    public void Play()
    {
        size = sounds.Length;
        chanceAll = 0;
        chosenClip = null;

        // sum up the chances
        for(int i=0; i<size; i++)
        {
            chanceAll += sounds[i].chance;
        }

        // choose a clip
        int j = 0;
        while (!chosenClip)
        {
            if(Random.Range(0, chanceAll) < sounds[j].chance)
            {
                chosenClip = sounds[j].clip;
            }

            chanceAll -= sounds[j].chance;
            j++;
        }

        // generate the clip
        AudioSource.PlayClipAtPoint(chosenClip, Camera.main.transform.position, Random.Range(volumeMin, volumeMax));
    }

}
