using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScene()
    {
        
         SceneManager.LoadSceneAsync("appartement");
        
    }
}
