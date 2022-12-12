
using UnityEngine;//这个组件里面已经包含
using UnityEngine.Audio;//不一定必须声明（二选一即可）
 
public class Audio : MonoBehaviour
{
    public AudioClip clip;//指定播放的声音
    public AudioSource source;//定义该AudioSource才能使用
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();//获取当前音频组件
    }
 
    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            source.Play(); //调用此音频文件播放
        }
    }*/

    void OnMouseDown(){  
	transform.GetComponent<BoxCollider>().enabled = false;
	source.Play();

	}

}


