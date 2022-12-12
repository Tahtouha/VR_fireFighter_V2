using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//must have AudioSource
 [RequireComponent(typeof(AudioSource))]
public class StepSound : MonoBehaviour
{
    public CharacterController cc;
    public AudioSource audio;
    //split sound source
    public AudioClip[] stepSounds;
    
    //Sound interval
    //public float stepLength = 0.4f;

    // Start is called before the first frame update
    void Start()
    {

	StartCoroutine(PlaySound());
    }
	IEnumerator PlaySound()
	{
	while(true){
		if(cc.isGrounded && cc.velocity.magnitude>0.3f)
		{
			//Randomly selected sound clips
			AudioClip acp=stepSounds[Random.Range(0,stepSounds.Length)];
			//play the step sound
			audio.PlayOneShot(acp);
			//delay
			yield return new WaitForSeconds(acp.length);
		}
		else{
			yield return null;
		}
	}        
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
