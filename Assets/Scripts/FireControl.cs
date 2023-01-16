using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
	//一定时间间隔内粒子碰撞计数
	int ParticleCollisionCount;
	//更新粒子碰撞计数时间间隔
	[SerializeField] float PCCupdataTime = 1.0f;
	//粒子碰撞计数转换为熄灭系数的转换系数
	[SerializeField] float PCCtoExtinguishRateRatio = 1.0f;
	//火焰燃烧 熄灭需要时间
	[SerializeField] float FireStartingTime = 2.0f;

	[SerializeField] float ExtinguishRate  = 20.0f;
	//火焰粒子效果
	[SerializeField] ParticleSystem FireParticleSystem;
	//烟雾粒子效果
	[SerializeField] ParticleSystem SmokeParticleSystem;
	//燃烧音效
	[SerializeField] AudioSource FireSource;

	protected bool isExtinguish;
	//当发生碰撞时候调用的函数
	public void HitByExtinguishParticleCollision(int points)
		{
			//粒子碰撞计数			
			ParticleCollisionCount +=points;
			//更新熄灭系数
			ExtinguishRate = ParticleCollisionCount * PCCtoExtinguishRateRatio;
		}


	//定时清零粒子碰撞计数和熄灭系数
	IEnumerator ResetPCC()
		{
			//等待指定时间间隔
			yield return new WaitForSeconds(PCCupdataTime);
			//清空计数
			ParticleCollisionCount = 0;
			//更新熄灭系数
			ExtinguishRate = 0;
			//再次启用本程序
			StartCoroutine(ResetPCC());
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


    // Start is called before the first frame update
    	void Start()
    		{
        			isExtinguish = true;
			FireParticleSystem.Stop();
			SmokeParticleSystem.Stop();
			//启动火函数,扩散；时间由成员变量FireStartingTime决定
			StartCoroutine(StartingFire());
			//启动定时清零粒子碰撞计数
			StartCoroutine(ResetPCC());
    		}

    // Update is called once per frame
    void Update()
    {
        
    }
}
