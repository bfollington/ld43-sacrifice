using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Textbox : MonoBehaviour
{
	public Image Bg;
	public TextMeshProUGUI Text;
	public Image MoreIndicator;
	
	// Use this for initialization
	void Start () {
		Hide();
	}

	private void Show()
	{
		GetComponent<CanvasGroup>().alpha = 1;
	}

	private void Hide()
	{
		GetComponent<CanvasGroup>().alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Say(string text)
	{
		Show();
		Text.text = text;
	}

	public void Done()
	{
		Hide();
	}
}
