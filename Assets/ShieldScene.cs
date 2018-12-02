using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class ShieldScene : MonoBehaviour
{
	public Textbox TextBox;
	public Checklist Checklist;
	public PlayerController Player;
	public SpriteRenderer Sprite;
	private bool fired = false;
	
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
		
		TextBox.Say("Got the shield!");
		TextBox.transform.localScale = new Vector3(1.2f, 1.2f, 1);
		Tween.LocalScale(TextBox.transform, new Vector3(1, 1, 1), 0.6f, 0, Tween.EaseBounce);
		yield return new WaitForSeconds(0.3f);
		Sprite.enabled = false;
		Player.GetShield();
		yield return new WaitForSeconds(0.3f);
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		
		Checklist.GetShield();
		
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
