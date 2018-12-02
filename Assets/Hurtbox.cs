using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{

	public float Damage = 1f;
	private int _frameCounter = 0;
	public LayerMask LayerMask;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		_frameCounter++;

		if (_frameCounter > 1)
		{
			Debug.Log("Delet!");
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Hit!");
        var target = other.GetComponent<IHurtable>();
		Debug.Log(target);
		Debug.Log(LayerMask == (LayerMask | (1 << other.gameObject.layer)));
		if (target != null && LayerMask == (LayerMask | (1 << other.gameObject.layer)))
		{
			target.Hurt(Damage);
		}
	}
}
