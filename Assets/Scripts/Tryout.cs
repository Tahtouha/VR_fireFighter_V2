using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tryout : MonoBehaviour
{
    
    public bool testSoundOff;
    private AudioSource[] everySound;
    
    public static Tryout Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        everySound = GameObject.FindObjectsOfType<AudioSource>();
        
    }

    public void setAudioOff(bool boolean)
    {
        testSoundOff = boolean;
    }
    
    public bool getAudioOff()
    {
        return testSoundOff;
    }

    // Update is called once per frame
    private void Awake()
    {
        Instance = this;
    }

    public void getAllSound()
    {
        everySound = GameObject.FindObjectsOfType<AudioSource>();
    }

    private void Update()
    {
        if (testSoundOff)
        {
            foreach (var sound in everySound)
            {
                sound.mute = true;
            }
        } 
    }
}
