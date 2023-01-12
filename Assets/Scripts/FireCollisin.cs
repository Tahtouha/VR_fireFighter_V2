using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollisin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("OtherObject"))
        {
            Debug.Log("Collision detected with: " + collision.gameObject.name);
        }
    }
}
