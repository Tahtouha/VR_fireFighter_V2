using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GlobalLightning : MonoBehaviour
{

    private GameObject[] InsideLights;

    private float[] LightsRanges;
    // Start is called before the first frame update
    private float flickersize = 13f;

    private Light light1;
    private Light light2;
    private Light spotlight;
    private AudioSource audiol1;
    private AudioSource audiol2;
    private float range1;
    private float range2;
    private float spotRange;
    private float spotIntensity;
    
    private float timer;
    private float timer2;
    private int randomLights;
    
    private IEnumerator coroutine1;
    private IEnumerator coroutine2;
    private IEnumerator coroutineSpot;
    private IEnumerator coroutine3;
    private IEnumerator coroutine4;
    private IEnumerator coroutine5;
    
    private float counter;
    private float counter2;
    void Start()
    {
        Object[] lights = GameObject.FindObjectsOfType(typeof(Light));
        timer = 8f;
        timer2 = 10f;
        
        /*if (lights.Length != 0)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                InsideLights[i] = lights[i].GameObject();
                LightsRanges[i] = lights[i].GetComponent<Light>().range;
            }
        }*/

        //randomLights = Random.Range(0, InsideLights.Length - 1);
        light1 = GameObject.Find("Point Light").GetComponent<Light>();
        light2 = GameObject.Find("Point Light (1)").GetComponent<Light>();
        audiol1 = GameObject.Find("Point Light").GetComponent<AudioSource>();
        audiol2 = GameObject.Find("Point Light (1)").GetComponent<AudioSource>();
        spotlight = GameObject.Find("Spot Light").GetComponent<Light>();
        range1 = light1.GetComponent<Light>().range;
        range2 = light2.GetComponent<Light>().range;
        spotRange = spotlight.GetComponent<Light>().range;
        spotIntensity = spotlight.GetComponent<Light>().intensity;
        coroutine1 = Clignotte(light1, range1, audiol1, Random.Range(0.5f, 10));
        coroutine2 = Clignotte(light2, range2, audiol2, Random.Range(0.5f, 10));
        coroutineSpot = Clignotte(spotlight, spotRange, null, Random.Range(0.5f, 10));
        counter = 0;
        counter2 = 0;
        Object[] _audio = GameObject.FindObjectsOfType(typeof(AudioSource));

    }

    // Update is called once per frame
    void Update()
    {
        coroutine1 = Clignotte(light1, range1, audiol1,Random.Range(0.5f, 10));
        coroutine2 = Clignotte(light2, range2, audiol2,Random.Range(0.5f, 10));
        coroutineSpot = Clignotte(spotlight, spotRange, null,Random.Range(0.5f, 10));
        coroutine5 = Sometimes(counter, timer, null, coroutineSpot);

        StartCoroutine(Sometimes(counter, timer, audiol1, coroutine1));
        StartCoroutine(Sometimes(counter2, timer2, audiol2, coroutine2));
        StartCoroutine(coroutine5);

    }

    private void OnApplicationQuit()
    {
        StopCoroutine(coroutine5);
    }

    IEnumerator Clignotte(Light light, float range, AudioSource audio, float time)
    {
        AudioSource _audioSource = null;

        if (audio!= null)
        {
            audio.mute = false;
        }
        if (light.range == range)
        {
            light.range = flickersize;
            if (audio!=null)
            {
                audio.Play();
            }

            if (light = spotlight)
            {
                light.intensity = 0.1f;
            }
            
        }
        else
        {
            light.range = range;
            light.intensity = 1f;
        }
        yield return new WaitForSeconds(time);
    }

    IEnumerator Sometimes(float counterr, float timerr, AudioSource audio, IEnumerator coroutine)
    {
        
        StartCoroutine(coroutine);
        yield return new WaitForSeconds(25);
    }
    
}