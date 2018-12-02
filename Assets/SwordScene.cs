﻿using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class SwordScene : MonoBehaviour
{
	public Textbox TextBox;
	public Checklist Checklist;
	public PlayerController Player;
	private bool fired = false;
	public SpriteRenderer Sprite;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BeginScene()
	{
		StartCoroutine(Scene());
	}

	IEnumerator Scene()
	{
		Player.DisableControl();
		Player.Stop();
		yield return new WaitForSeconds(0.2f);
		
		TextBox.Say("Got the sword!");
		TextBox.transform.localScale = new Vector3(1.2f, 1.2f, 1);
		Tween.LocalScale(TextBox.transform, new Vector3(1, 1, 1), 0.6f, 0, Tween.EaseBounce);
		yield return new WaitForSeconds(0.3f);
		Sprite.enabled = false;
		Player.GetSword();
		yield return new WaitForSeconds(0.3f);
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		
		Checklist.GetSword();
		
		TextBox.Done();
		Player.EnableControl();
		Player.EnableAnimation();
		
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		var player = other.GetComponent<PlayerController>();

		if (!fired && player != null)
		{
			fired = true;
			BeginScene();
		}
	}
}
