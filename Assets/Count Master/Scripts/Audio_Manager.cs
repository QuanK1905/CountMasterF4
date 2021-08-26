using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum AudioType { Music, Sound }

public class Audio_Manager : MonoBehaviour
{
    public Sound[] sounds;
    public bool soundMute = false, musicMute = false;

    #region singleton
    public static Audio_Manager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        // DontDestroyOnLoad(this);
    }

    #endregion

    void Start()
    {

        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.Clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;

            s.Source = source;

            if (s.playOnStart)
                s.Source.Play();
        }
    }

    public void play(string soundName)
    {
        
        Sound s = Array.Find(sounds, so => so.name == soundName);

        if (s == null)
        {
            // Debug.LogError("Sound with name " + soundName + " doesn't exist!");
            return;
        }

        if (s.audioType == AudioType.Music && !musicMute)
        {
            s.Source.Play();
        }
        else if (s.audioType == AudioType.Sound && !soundMute)
        {
            s.Source.Play();
        }

    }

    public void stop(string soundName)
    {

        Sound s = Array.Find(sounds, so => so.name == soundName);

        if (s == null)
        {
            Debug.LogError("Sound with name " + soundName + " doesn't exist!");
            return;
        }

        s.Source.Stop();

    }
}

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioType audioType;
        public AudioClip Clip;


        [HideInInspector]
        public AudioSource Source;

        public float volume = 1;
        public float pitch = 1;

        public bool loop = false;
        public bool playOnStart = false;

    }






