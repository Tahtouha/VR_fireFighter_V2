using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollisin: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnParticleCollision(GameObject waterObj)
	{
		//Debug.Log(waterObj);
		ParticleSystem ps = transform.GetComponent<ParticleSystem>();
		ps.Stop(true);
	}
}
