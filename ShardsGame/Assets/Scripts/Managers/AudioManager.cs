using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds; // Here is our sounds array

    [SerializeField] private Sound typeSound;
  //public Sound[] walkingClips; // custom sounds array for spesific sounds

    public AudioClip[] mainMusicClips; // Main game music(ambient)
  




    void Update()
    {
        // Playing the main music, can input many tracks so it will get one randomly each time
        if(!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().clip = GetRandomClip();
            gameObject.GetComponent<AudioSource>().Play();
            
        }
    }
    public AudioClip GetRandomClip()
    {   // Get random main music clip
            return mainMusicClips[Random.Range(0,mainMusicClips.Length)];
    }
    public void Play(AudioSource source, Sound snd)
    { 
      Sound s = Array.Find(sounds, sound => sound == snd); 
      source.clip = s.clip; 
      source.Play(); 
    }

    public void PlaySound()
    {
      AudioSource audioSource = gameObject.GetComponent<AudioSource>();
      audioSource.Stop();
      audioSource.clip = typeSound.clip;
      audioSource.Play();
        
    }
    
    /*
     // How to call from other script:
      //Play sound

          if(!gameObject.GetComponent<AudioSource>().isPlaying)
           {
          AudioManager audioManager = GameObject.Find("/GameManager").GetComponent<AudioManager>();
          Sound snd = audioManager.sounds[Random.Range(0,audioManager.sounds.Length)];
          audioManager.Play(gameObject.GetComponent<AudioSource>(), snd);
           }

     //Example in FourDirectionalMovement script
    */
    
}
