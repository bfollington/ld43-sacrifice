using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checklist : MonoBehaviour
{
	public Image SwordStrike;
	public Image ShieldStrike;

	private bool _hasSword = false;
	private bool _hasShield = false;
	
	// Use this for initialization
	void Start () {
		SwordStrike.canvasRenderer.SetAlpha(0);
		ShieldStrike.canvasRenderer.SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (_hasSword)
		{
			SwordStrike.canvasRenderer.SetAlpha(1);
		}
		
		if (_hasShield)
		{
			ShieldStrike.canvasRenderer.SetAlpha(1);
		}
	}

	public void GetSword()
	{
		_hasSword = true;
	}

	public void GetShield()
	{
		_hasShield = true;
	}

	public bool IsComplete
	{
		get {
			return _hasShield && _hasShield;
		}
	}

	public bool HasSword
	{
		get { return _hasSword; }
	}
}
