using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Pixelplacement;
using UnityEngine;

public class Breakable : MonoBehaviour, IHurtable
{

	public float Health;
	public SpriteRenderer Sprite;
	public AudioSource HitSound;
	public AudioSource BreakSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hurt(float damage)
	{
		Health--;
		Sprite.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		Tween.LocalScale(Sprite.transform, new Vector3(1, 1, 1), 0.5f, 0, Tween.EaseBounce);
		Debug.Log("OW!");

		if (Health <= 0)
		{
			BreakSound.Play();
			Destroy(gameObject);
		}
		else
		{
			HitSound.Play();
		}
	}
}
