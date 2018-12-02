using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour {
	private Animator _animator;

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
		_animator.Play("idle");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GrabHeart()
	{
		_animator.Play("heart_grab");
	}
}
