using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject follower;
    public bool behindyou = false;
    public bool stareatyou = false;
    public bool isshy = false;
    public LayerMask mask;
    public bool OnInRange;
    public bool OffInrange;
    public float range = 0.5f;
    public Audio flicker;

    private GameObject[] player;

    private GameObject root;

    private GameObject[] followerLights;

    private float flickersize = 0.5f;

    private float[] lightRanges;

    private float timer;

    private float timer2 = 1.5f;
    
    private IEnumerator coroutine;
    
    private IEnumerator coroutine2;

    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("MainCamera");
        root = follower.transform.root.gameObject;
        followerLights = getfollowerLight();
        print(followerLights);
        lightRanges = getLightRange();
        timer = 2*Random.value;
        coroutine = clignotte();
        counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (stareatyou)
        {
            fixerJoueur();
        }
        
        if (behindyou)
        {
            suivreJoueur();
        }

        if (OnInRange)
        {
            lightOnWhenInrange();
        }

        if (OffInrange)
        {
            lightOffWhenInrange();
        }

        if (isshy)
        {
            if (followerLights.Length > 0)
            {
                if (isPlayerLooking())
                {
                    if (followerLights[0].GetComponent<Light>().range != 0 && counter < timer2)
                    {
                        counter += Time.deltaTime;
                        coroutine = clignotte();
                        StartCoroutine(coroutine);
                        timer = 2*Random.value;
                    }

                    if (counter >= timer2)
                    {
                        StopCoroutine(coroutine);
                        followerLights[0].GetComponent<Light>().range = 0;
                    }
                }
                else
                {
                    counter = 0;
                    coroutine = clignotte();
                    StopCoroutine(coroutine);
                    followerLights[0].GetComponent<Light>().range = lightRanges[0];
                }
            }
        }
    }
    
    Vector3 getPlayerDirection()
    {
        if (player.Length < 2)
        {
            Vector3 _player_pose = player[0].transform.position;
            Vector3 _follower_pose =  follower.transform.position;
            Vector3 _direction = _follower_pose - _player_pose;
            return _direction;
        }
        else
        {
            return new Vector3();
        }
    }

    bool isPlayerLooking()
    {
        RaycastHit hit;
        bool islooking = false;
        if(Physics.SphereCast(player[0].transform.position,0.1f , player[0].transform.forward, out hit, Mathf.Infinity, mask))
        {
            var obj = hit.collider.gameObject;
            if (Vector3.Distance(hit.point, follower.transform.position) < 2f)
            {
                islooking = true;
            }
        }
        return islooking;
    }

    GameObject[] getfollowerLight()
    {
        bool haslight = false;
        int i = 0;
        GameObject[] Followerlights = new GameObject[10];
        Object[] lights = FindObjectsOfType(typeof(Light));
        if (lights.Length != 0)
        {
            foreach (var light in lights)
            {
                haslight = light.GameObject().transform.IsChildOf(follower.transform);
                if (haslight == true|| light.GameObject() == follower)
                {
                    Followerlights[i] = light.GameObject();
                    i++;
                }
            }
        }
        List<GameObject> gameObjectList = new List<GameObject>(Followerlights);
        gameObjectList.RemoveAll(x => x == null);
        Followerlights = gameObjectList.ToArray();
        return Followerlights;
    }

    float[] getLightRange()
    {
        float[] ranges = new float[followerLights.Length];
        int i = 0;
        foreach (var light in followerLights)
        {
            ranges[i] = light.GetComponent<Light>().range;
            i++;
        }

        return ranges;
    }

    void lightOnWhenInrange()
    {
        foreach (var light in followerLights)
        {
            if (getDistance() < range)
            {
                light.GetComponent<Light>().enabled = true;
                light.SetActive(true);
            }
        }
    }

    void lightOffWhenInrange()
    {
        foreach (var light in followerLights)
        {
            if (getDistance() < range)
            {
                AudioSource _audioSource;
                _audioSource = light.GetComponent<AudioSource>();
                light.GetComponent<Light>().enabled = false;
                _audioSource.mute = true;
            }
        }
    }

    float getDistance()
    {
        Vector3 _player_pose = player[0].transform.position;
        Vector3 _follower_pose =  follower.transform.position;
        float _distance = Vector3.Distance(_follower_pose, _player_pose);
        if (_distance < 0)
        {
            _distance = -_distance;
        }

        return _distance;
    }

    IEnumerator clignotte()
    {
        AudioSource _audioSource;
        for (int i = 0; i < followerLights.Length; i++)
        {
            _audioSource = followerLights[i].gameObject.GetComponent<AudioSource>();
            if (_audioSource != null)
            {
                _audioSource.mute = false;
            }
            if (followerLights[i].GetComponent<Light>().range == lightRanges[i])
            {
                followerLights[i].GetComponent<Light>().range = flickersize;
                _audioSource.Play();
            }
            else
            {
                followerLights[i].GetComponent<Light>().range = lightRanges[i];
            }
            yield return new WaitForSeconds(timer);
        }
    }

    void fixerJoueur()
    {
        Vector3 _direction = getPlayerDirection();
        if (_direction != null)
        {
            follower.transform.rotation = Quaternion.LookRotation(_direction);
            follower.transform.Rotate(0,180,0);
        }
    }

    void suivreJoueur()
    {
        Vector3 _direction = getPlayerDirection();
        if (_direction != null)
        {
            float _distance = getDistance();
            if (_distance > 2)
            { 
                root.transform.Translate(-_direction * Time.deltaTime);
            }
        }
    }
}
