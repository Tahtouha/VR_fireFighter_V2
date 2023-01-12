using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalLigtning : MonoBehaviour
{

    private GameObject[] InsideLights;

    private float[] LightsRanges;
    // Start is called before the first frame update
    private float flickersize = 0.1f;

    private GameObject light1;
    private GameObject light2;
    private float range1;
    private float range2;
    
    private float timer;
    private float timer2;
    private int randomLights;
    
    private IEnumerator coroutine1;
    private IEnumerator coroutine2;
    
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
        range1 = light1.GetComponent<Light>().range;
        range2 =light2.GetComponent<Light>().range;
        coroutine1 = Clignotte(light1, range1);
        coroutine2 = Clignotte(light2, range2);
        counter = 0;
        counter2 = 0;
        Debug.Log(light1);
        Debug.Log(light2);
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
            coroutine1 = Clignotte(light1, range1);
            StartCoroutine(coroutine1);
        }
        else
        {
            counter = 0;
            StopCoroutine(coroutine1);
        }
        
        if (counter2 < timer2)
        {
            counter2 += Time.deltaTime;
            coroutine2 = Clignotte(light2, range2);
            StartCoroutine(coroutine2);
        }
        else
        {
            counter2 = 0;
            StopCoroutine(coroutine2);
        }
        
    }

    IEnumerator Clignotte(GameObject light, float range)
    {
        AudioSource _audioSource;
        _audioSource = light.gameObject.GetComponent<AudioSource>();
        if (_audioSource != null)
        {
            _audioSource.mute = false;
        }
        if (light.GetComponent<Light>().range == range)
        {
            light.GetComponent<Light>().range = flickersize;
            _audioSource.Play();
        }
        else
        {
            light.GetComponent<Light>().range = range;
        }
        yield return new WaitForSeconds(2*Random.value);
    }
}
