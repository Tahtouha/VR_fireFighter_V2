using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extincteur : MonoBehaviour
{
    private GameObject extincteur;

    private GameObject[] fires;
    // Start is called before the first frame update
    void Start()
    {
        extincteur = this.gameObject;
        fires = GameObject.FindGameObjectsWithTag("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isFireInsight()
    {
        
        return false;
    }
}
