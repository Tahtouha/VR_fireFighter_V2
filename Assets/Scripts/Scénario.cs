using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Scénario : MonoBehaviour
{
    private float targetZmax = 6.75f;

    private float targetZmin = 6.25f;

    private float targetXmax = -1.1f;

    private float targetXmin = -1.7f;

    private float targetYmax = 1.15f;

    private float distanceQuiet = 1f;

    private float distanceRemiOut = 3f;

    private GameObject[] props;

    private GameObject phone;

    private AudioSource ring;

    private GameObject player;

    private GameObject remi;

    private GameObject[] fires;

    private AudioSource alarm;

    private bool shenanigan;

    private bool boom;
    
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        props = GameObject.FindGameObjectsWithTag("Props");
        phone = GameObject.Find("phone");
        player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        remi = GameObject.Find("Rémi Mollette");
        /*fires = GameObject.FindGameObjectsWithTag("fire");*/
        alarm = GameObject.Find("alarm").GetComponent<AudioSource>();
        shenanigan = false;
        boom = false;
        ring = phone.GetComponent<AudioSource>();
        Debug.Log("haha" + phone);
        ring.mute = true;
        ring.loop = true;
        alarm.loop = true;
        alarm.mute = true;
        coroutine = Flee();
        RenderSettings.fog = false;
        foreach (var _fire in fires)
        {
            _fire.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!shenanigan)
        {
          doDishes();  
        }
        else
        {
            Debug.Log(Vector3.Distance(player.transform.position, phone.transform.position));
            if (isAtDistanceOfPhone(distanceQuiet))
            {
                
                ring.mute = true;
                alarm.mute = false;
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
            foreach (var _fire in fires)
            {
                _fire.SetActive(true);
            }
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
            Debug.Log("ici");
            shenanigan = true;
            ring.mute = false;
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
            remi.transform.Translate(0.015f,0,0);
        }
        yield return new WaitForSeconds(Time.deltaTime);
    }

}

