using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalLightning : MonoBehaviour
{

    private GameObject[] InsideLights;

    private float[] LightsRanges;
    // Start is called before the first frame update
    private float flickersize = 0.1f;

    private GameObject light1;
    private GameObject light2;
    private GameObject spotlight;
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
    
    private float counter;
    private float counter2;
    void Start()
    {
        Object[] lights = GameObject.FindObjectsOfType(typeof(Light));
        timer = 1.5f;
        timer2 = 1.8f;
        
        /*if (lights.Length != 0)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                InsideLights[i] = lights[i].GameObject();
                LightsRanges[i] = lights[i].GetComponent<Light>().range;
            }
        }*/

        //randomLights = Random.Range(0, InsideLights.Length - 1);
        light1 = GameObject.Find("Point Light");
        light2 = GameObject.Find("Point Light (1)");
        spotlight = GameObject.Find("Spot Light");
        Debug.Log(spotlight);
        range1 = light1.GetComponent<Light>().range;
        range2 = light2.GetComponent<Light>().range;
        spotRange = spotlight.GetComponent<Light>().range;
        spotIntensity = spotlight.GetComponent<Light>().intensity;
        coroutine1 = Clignotte(light1, range1, 2*Random.value);
        coroutine2 = Clignotte(light2, range2, 2*Random.value);
        coroutineSpot = Clignotte(spotlight, spotRange, 10 * Random.value);
        counter = 0;
        counter2 = 0;
        Object[] _audio = GameObject.FindObjectsOfType(typeof(AudioSource));
        /*
        foreach (var son in _audio)
        {
            Debug.Log(son.GameObject().transform.parent.gameObject.name);
        }
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < timer)
        {
            counter += Time.deltaTime;
            coroutine1 = Clignotte(light1, range1, 2*Random.value);
            StartCoroutine(coroutine1);
            coroutineSpot = Clignotte(spotlight, spotRange, 10 * Random.value);
            StartCoroutine(coroutineSpot);
        }
        else
        {
            counter = 0;
            StopCoroutine(coroutine1);
            StopCoroutine(coroutineSpot);
        }
        
        if (counter2 < timer2)
        {
            counter2 += Time.deltaTime;
            coroutine2 = Clignotte(light2, range2, 2*Random.value);
            StartCoroutine(coroutine2);
        }
        else
        {
            counter2 = 0;
            StopCoroutine(coroutine2);
        }
        
    }

    IEnumerator Clignotte(GameObject light, float range, float time)
    {
        AudioSource _audioSource = null;
        Light component = light.GetComponent<Light>();
        
        if (light.gameObject.GetComponent<AudioSource>()!= null)
        {
            _audioSource = light.gameObject.GetComponent<AudioSource>();
            _audioSource.mute = false;
        }
        if (light.GetComponent<Light>().range == range)
        {
            component.range = flickersize;
            if (_audioSource!=null)
            {
                _audioSource.Play();
            }

            if (light = spotlight)
            {
                component.intensity = 0.1f;
            }
            
        }
        else
        {
            component.range = range;
            component.intensity = 1f;
        }
        yield return new WaitForSeconds(time);
    }
}