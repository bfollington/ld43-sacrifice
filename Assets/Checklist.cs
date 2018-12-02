using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checklist : MonoBehaviour
{
	public Image SwordStrike;
	public Image ShieldStrike;

	private bool HasSword = false;
	private bool HasShield = false;
	
	// Use this for initialization
	void Start () {
		SwordStrike.canvasRenderer.SetAlpha(0);
		ShieldStrike.canvasRenderer.SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (HasSword)
		{
			SwordStrike.canvasRenderer.SetAlpha(1);
		}
		
		if (HasShield)
		{
			ShieldStrike.canvasRenderer.SetAlpha(1);
		}
	}

	public void GetSword()
	{
		HasSword = true;
	}

	public void GetShield()
	{
		HasShield = true;
	}

	public bool IsComplete
	{
		get {
			return HasShield && HasShield;
		}
	}
}
