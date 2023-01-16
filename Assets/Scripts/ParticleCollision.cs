using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{

	//定义一个存放粒子碰撞事件的列表
	private List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();
	//用于存放粒子系统组件的变量
	private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
     	_particleSystem = GetComponent<ParticleSystem>();
    }
	
	private void OnParticleCollision (GameObject other)
		{

		Debug.Log("APRTICLE HIT AHHHHH");
			/*//用于存放被碰撞对象firecontrol的变量
			FireControl firehit = null;
			//用于存放触发一次事件时与火焰对象碰撞的粒子数量变量
			int hitCount = 0;
			//获取同时与被碰撞对象发生碰撞的所有粒子碰撞事件列表
			int numCollisionEvents = _particleSystem.GetCollisionEvents(other, CollisionEvents);
			//统计发生碰撞的所有例子中，与火焰对象碰撞的粒子数量
			for(int i=0; i<numCollisionEvents; i++)
			{
				var col = CollisionEvents[i].colliderComponent;
				var fire = col.GetComponent<FireControl>();
				if(fire != null)
				{
					hitCount++;
					firehit = fire;
				}
			}
			if(firehit !=null )
			{
				firehit.HitByExtinguishParticleCollision(hitCount);
			}*/
		}

    // Update is called once per frame
    void Update()
    {
        
    }
}
