using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool menuOn = true;
    private GameObject menu;
    private Transform parent;
    private Vector3 position;
    private bool check;

    void Start()
    {
        menu = GameObject.FindWithTag("Menu");
        parent = menu.transform.root;
        check = menuOn;
    }

    private void Update()
    {
        menu.SetActive(menuOn);
        Debug.Log(menu);

        if (Input.GetKeyDown("p"))
        {
            menuOn = !menuOn;
            parent.position = position;
        }

        if (menuOn != check && menuOn)
        {
            position = parent.position;
        }
    
        if (menuOn)
        {
            parent.position = position;
        }
    }

    public void commencer()
    {
        menuOn = false;
    }

    public void quitter()
    {
        Application.Quit();
    }
}
