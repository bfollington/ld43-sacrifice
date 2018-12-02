using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class TestScene : MonoBehaviour
{
	public Textbox TextBox;
	public Checklist Checklist;
	public PlayerController Player;
	private bool fired = false;
	private AudioSource _beginSfx;

	// Use this for initialization
	void Start ()
	{
		_beginSfx = GetComponent<AudioSource>();
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
		
		TextBox.Say("Hey!");
		TextBox.transform.localScale = new Vector3(1.2f, 1.2f, 1);
		Tween.LocalScale(TextBox.transform, new Vector3(1, 1, 1), 0.6f, 0, Tween.EaseBounce);
		yield return new WaitForSeconds(0.6f);
//		Tween.Rotate(TextBox.transform, new Vector3(0, 0, -5), Space.Self, 1f, 0, Tween.EaseBounce);
//		yield return new WaitForSeconds(1f);
		
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		yield return new WaitForSeconds(0.1f);
		
		TextBox.Say("Have you not prepared for the ritual?");
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		yield return new WaitForSeconds(0.1f);
		
		TextBox.Say("Manik hungers, AND WE MUST FEED HIM!");
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		yield return new WaitForSeconds(0.1f);
		
		TextBox.Say("Fetch the ritual items and return to me.");
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		yield return new WaitForSeconds(0.1f);

		_beginSfx.Play();
		Checklist.gameObject.SetActive(true);
		
		TextBox.Done();
		Player.EnableControl();
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
