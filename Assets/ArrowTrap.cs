using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
	public GameObject Arrow;
	public float Delay;
	public float Frequency = 3;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(FireArrows());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator FireArrows() {
		yield return new WaitForSeconds(Delay);
		for(;;) {
			// execute block of code here
			yield return new WaitForSeconds(Frequency);
			var arrow = Instantiate(Arrow);
			arrow.transform.position = transform.position + new Vector3(0, 0.5f, 0);
		}
	}
}
