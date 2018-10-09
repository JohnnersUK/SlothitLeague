using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    
    
    // Use this for initialization
	void Awake () {
        
        //Adds all sounds in audiomanager as sound sources in the game
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.mute = s.mute;
            s.source.loop = s.loop;
        }
	}
	
	// Update is called once per frame
	public void Play (string name) {
        {
            Sound s = Array.Find(sounds, Sound => Sound.name == name);
            
    
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            if (s.source)
            {
                s.source.Play();
            }
        }
		
	}

    public void Mute(string name, bool mute)
    {
        {
            Sound s = Array.Find(sounds, Sound => Sound.name == name);


            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            if(mute == true)
            {
                if (s.source)
                {
                    s.source.Pause();
                }
            }
            else
            {
                if (s.source)
                {
                    s.source.Play();
                }
            }
            
        }

    }
}
