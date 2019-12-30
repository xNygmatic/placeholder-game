using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string clipName)
    {
        Sound sound = null;
        foreach (Sound s in sounds)
        {
            if (s.name == clipName)
            {
                sound = s;
            }
        }
        if (sound != null)
        {
            switch (sound.loop)
            {
                case true:
                    sound.source.Play();
                    break;

                case false:
                    sound.source.PlayOneShot(sound.clip);
                    break;
            }
        } else {
            Debug.Log("Sound: " + clipName + " does not exist!");
        }
    }
}
