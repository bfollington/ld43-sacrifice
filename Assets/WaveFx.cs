using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;

public class WaveFx : MonoBehaviour
{
	public float Delay;
	private Vector2 initialPosition;
	private RectTransform rect;
	
	// Use this for initialization
	void Start ()
	{
		rect = GetComponent<RectTransform>();
		initialPosition = rect.anchoredPosition;
		StartCoroutine(Wave());
	}
	
	IEnumerator Wave() {
		yield return new WaitForSeconds(Delay);
		for(;;) {
			// execute block of code here
			Tween.AnchoredPosition(rect, initialPosition + Vector2.down * 5, 1f, 0, Tween.EaseInOutStrong);
			yield return new WaitForSeconds(1f);
			
			Tween.AnchoredPosition(rect, initialPosition - Vector2.down * 5, 1f, 0, Tween.EaseInOutStrong);
			yield return new WaitForSeconds(1f);
		}
	}
}
