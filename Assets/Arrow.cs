using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	public LayerMask LayerMask;
	public Vector3 FireDirection;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += FireDirection.normalized * 8 * Time.deltaTime;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Hit Arrow!");
		var target = other.GetComponent<IHurtable>();
		Debug.Log(target);
		Debug.Log(LayerMask == (LayerMask | (1 << other.gameObject.layer)));
		if (target != null && LayerMask == (LayerMask | (1 << other.gameObject.layer)))
		{
			target.Hurt(1);
			Destroy(gameObject);
		}
	}
}
