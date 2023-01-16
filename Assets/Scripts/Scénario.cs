using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scénario : MonoBehaviour
{
    public GameObject fire;

    public Material black;

    public Material wanted;

    public Material tuto;

    public Material part;

    private float targetZmax = 6.8f;

    private float targetZmin = 6.2f;

    private float targetXmax = -0.95f;

    private float targetXmin = -1.75f;

    private float targetYmax = 1.5f;

    private float distanceQuiet = 1f;

    private float distanceRemiOut = 5f;

    private GameObject[] props;

    private GameObject phone;

    private GameObject extincteur;

    private GameObject magicHint;

    private AudioSource ring;

    private GameObject player;

    private GameObject remi;

    private Renderer tv;

    private Light magie;

    private ParticleSystem wow;

    private AudioSource alarm;

    private AudioSource muffled;

    private TMP_Text todo;

    private bool shenanigan;

    private bool boom;

    private IEnumerator coroutine;

    private float timer = 50000f;

    private float counter;

    private bool testSoundOff;

    private List<GameObject> fires;

    // Start is called before the first frame update
    void Start()
    {
        props = GameObject.FindGameObjectsWithTag("Props");
        phone = GameObject.Find("phone");
        extincteur = GameObject.Find("Fire Extinguisher");
        Debug.Log(extincteur);
        magicHint = GameObject.Find("magicHint");
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        tv = GameObject.Find("Screen").GetComponent<Renderer>();
        remi = GameObject.Find("Rémi Mollette");
        magie = magicHint.GetComponent<Light>();
        wow = magicHint.GetComponent<ParticleSystem>();
        alarm = GameObject.Find("alarm").GetComponent<AudioSource>();
        todo = GameObject.Find("TODO").GetComponent<TMP_Text>();
        shenanigan = false;
        boom = false;
        muffled = GameObject.Find("Screen").GetComponent<AudioSource>();
        ring = phone.GetComponent<AudioSource>();
        ring.mute = true;
        ring.loop = true;
        alarm.loop = true;
        alarm.mute = true;
        muffled.mute = true;
        muffled.playOnAwake = true;
        coroutine = Flee();
        RenderSettings.fog = false;
        tv.material = tuto;
        magie.spotAngle = 50f;
        counter = 0f;
        testSoundOff = Tryout.Instance.getAudioOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shenanigan)
        {
            if (counter < timer * Time.deltaTime)
            {
                counter++;
            }
            else
            {
                counter = 0;
                if (tv.material.name == tuto.name + " (Instance)")
                {
                    tv.material = part;
                }
                else tv.material = tuto;
            }
            doDishes();
        }
        else
        {
            if (isAtDistanceOfPhone(distanceQuiet))
            {

                ring.mute = true;
                alarm.mute = false;
                if (!boom)
                {
                    boom = true;
                    fires.Add(Instantiate(fire, new Vector3(-1f, 0, 5.5f), Quaternion.identity));
                    fires.Add(Instantiate(fire, new Vector3(0.3f, 0, 5.5f), Quaternion.identity));
                    fires.Add(Instantiate(fire, new Vector3(-1f, 0, 4.5f), Quaternion.identity));
                    fires.Add(Instantiate(fire, new Vector3(-2f, 1.5f, 5.4f), Quaternion.identity));
                    fires.Add( Instantiate(fire, new Vector3(0.5f, 1.5f, 6.2f), Quaternion.identity));
                    fires.Add(Instantiate(fire, new Vector3(-1f, 1f, 3.1f), Quaternion.Euler(90, 0, 0)));
                    if (testSoundOff)
                    {
                        Tryout.Instance.getAllSound();
                    }
                    float _y = magicHint.transform.position.y;
                    magicHint.transform.SetPositionAndRotation(new Vector3(extincteur.transform.position.x -0.2f, _y, extincteur.transform.position.z -0.2f), magicHint.transform.rotation);
                    magie.spotAngle = 20;
                    magie.intensity = 1.75f;
                }
                boom = true;
            }

            if (isAtDistanceOfPhone(distanceRemiOut))
            {
                coroutine = Flee();
                StartCoroutine(coroutine);
                if (remi.transform.position.z > -9f)
                {
                    StopCoroutine(coroutine);
                }
            }
        }

        if (boom)
        {
            RenderSettings.fog = true;
            if (fires.Count == 0)
            {
                SceneManager.LoadScene("MenuVR");
            }
            
            if (extincteur.transform.position.x != magicHint.transform.position.x)
            {
                magicHint.SetActive(false);
            }
            fires.RemoveAll(x => x == null);
        }
    }

    bool isInTarget(GameObject prop)
    {
        Vector3 _pose = prop.transform.position;
        if ((_pose.x <= targetXmax) && (_pose.x >= targetXmin) && (_pose.y <= targetYmax) && (_pose.z <= targetZmax) &&
            (_pose.z >= targetZmin))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void doDishes()
    {

        if (!shenanigan && isInTarget(props[0]) && isInTarget(props[1]) && isInTarget(props[2]))
        {

            shenanigan = true;
            ring.mute = false;
            float _y = magicHint.transform.position.y;
            magicHint.transform.SetPositionAndRotation(new Vector3(phone.transform.position.x -0.2f, _y, phone.transform.position.z -0.2f), magicHint.transform.rotation);
            magie.spotAngle = 20;
            magie.intensity = 1.75f;
            wow.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            todo.text="To Do:\n- f̶a̶i̶r̶e̶ ̶c̶u̶i̶r̶e̶ ̶l̶e̶s̶ ̶p̶a̶t̶e̶s̶ \n- M̶e̶t̶t̶r̶e̶ ̶l̶a̶ ̶v̶a̶i̶s̶s̶e̶l̶l̶e̶ ̶d̶a̶n̶s̶ ̶l̶'̶é̶v̶i̶e̶r̶\n- Changer les ampules qui clignottent";
            tv.material = wanted;
            muffled.mute = false;
        }
    }

    bool isAtDistanceOfPhone(float distance)
    {
        Vector3 _phonePose = phone.transform.position;
        Vector3 _playerPose = player.transform.position;
        float _distPP = Vector3.Distance(_phonePose, _playerPose);
        if (_distPP <= distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Flee()
    {
        if (remi.transform.position.z > -9f)
        {
            remi.transform.Translate(0.09f,0,0);
        }
        yield return new WaitForSeconds(Time.deltaTime);
    }

}
