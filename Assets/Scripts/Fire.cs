using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	//燃烧音效
	[SerializeField] AudioSource FireSource;
	//火焰粒子效果
	[SerializeField] ParticleSystem FireParticleSystem;
	//烟雾粒子效果
	[SerializeField] ParticleSystem SmokeParticleSystem;
	//火焰燃烧 熄灭需要时间
	[SerializeField] float FireStartingTime = 2.0f;
	//火焰生命值
	[SerializeField] float FireLife = 100f;
	//生命值上限
	[SerializeField] float MaxFireLife = 100f;
	//火焰回复系数
	[SerializeField] float RecoveryRate = 40f;
	//火焰熄灭系数
	[SerializeField] float ExtinguishRate  = 20.0f;
	//火焰是否熄灭
	protected bool isExtinguish;

	//一定时间间隔内粒子碰撞计数
	int ParticleCollisionCount;
	//更新粒子碰撞计数时间间隔
	[SerializeField] float PCCupdataTime = 1.0f;
	//粒子碰撞计数转换为熄灭系数的转换系数
	[SerializeField] float PCCtoExtinguishRateRatio = 1.0f;

    // Start is called before the first frame update
    	void Start()
    		{
        			isExtinguish = true;
			FireParticleSystem.Stop();
			SmokeParticleSystem.Stop();
			//启动火函数,扩散；时间由成员变量FireStartingTime决定
			StartCoroutine(StartingFire());
    		}
	IEnumerator StartingFire()
		{
			SmokeParticleSystem.Stop();
			FireParticleSystem.time = 0;
			FireParticleSystem.Play();
			//播放音效
			FireSource.Play();
			float elapsedTime = 0.0f;
			while (elapsedTime < FireStartingTime )
				{
					float ratio = Mathf.Min(1.0f, (elapsedTime / FireStartingTime ));
					FireParticleSystem.transform.localScale = Vector3.one*ratio;
					yield return null;
					elapsedTime += Time.deltaTime;
				}
			FireParticleSystem.transform.localScale = Vector3.one;
			isExtinguish = false;
		}
	IEnumerator Extinguish()
		{
			SmokeParticleSystem.Stop();
			FireSource.Stop();
			SmokeParticleSystem.time =0;
			SmokeParticleSystem.Play();

			yield return new WaitForSeconds(FireStartingTime);
			SmokeParticleSystem.Stop();
			FireParticleSystem.transform.localScale = Vector3.one;
		}

    // Update is called once per frame
    void Update()
    {
	//每一帧更新火势
	//如果没有熄灭，并且生命值没有达到最大或熄灭系数不为0
	if(!isExtinguish&& (FireLife< MaxFireLife || ExtinguishRate!=0f))
	{
		//根据系数计算当前生命
		FireLife += (RecoveryRate - ExtinguishRate)* Time.deltaTime;
		if(FireLife <=0)
		{
			FireLife =0f;
			isExtinguish = true;
			StartCoroutine(Extinguish());
		}
		//如果生命值打到上限，则维持
		if(FireLife >= MaxFireLife)
		{
			FireLife = MaxFireLife;
		}
		FireParticleSystem.transform.localScale = Vector3.one*FireLife/MaxFireLife;
	}
    }
}


        	/*void OnParticleCollision(GameObject waterObj)
	{
		//Debug.Log(waterObj);
		ParticleSystem ps = transform.GetComponent<ParticleSystem>();
		ps.Stop(true);
	}*/
