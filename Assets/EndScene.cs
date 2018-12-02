using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
	public Textbox TextBox;
	public Checklist Checklist;
	public PlayerController Player;
	public Animator PlayerAnimator;
	public Priest Priest;
	private bool fired = false;
	public AudioSource SacrificeSound;
	
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
		Checklist.gameObject.SetActive(false);
		
		Player.DisableControl();
		Player.Stop();
		yield return new WaitForSeconds(0.2f);
		
		TextBox.Say("Excellent.");
		TextBox.transform.localScale = new Vector3(1.2f, 1.2f, 1);
		Tween.LocalScale(TextBox.transform, new Vector3(1, 1, 1), 0.6f, 0, Tween.EaseBounce);
		yield return new WaitForSeconds(0.6f);
		
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		yield return new WaitForSeconds(0.1f);
		
		TextBox.Say("The time has come!");
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		yield return new WaitForSeconds(0.1f);
		TextBox.Done();

		Priest.GrabHeart();
		yield return new WaitForSeconds(0.5f);
		SacrificeSound.Play();
		Player.Sacrifice();
			
		yield return new WaitForSeconds(3f);
		
		TextBox.Say("Man, I love my job.");
		yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
		yield return new WaitForSeconds(0.1f);
		TextBox.Done();
		
		yield return new WaitForSeconds(2f);
		
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("End");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		
		Player.EnableControl();
	}

	private void OnTriggerEnter(Collider other)
	{
		var player = other.GetComponent<PlayerController>();

		if (!fired && player != null)
		{
			if (Checklist.IsComplete)
			{
				fired = true;
				BeginScene();
			}
		}
	}
}
