using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {
	
	public string[] scenarios;
	public Text uiText;
	
	int currentLine = 0;
	
	void Start()
	{
		TextUpdate();
	}
	
	void Update () 
	{
		if(currentLine < scenarios.Length && Input.GetMouseButtonDown(0))
		{
			TextUpdate();
		}
	}
	

	void TextUpdate()
	{
		uiText.text = scenarios[currentLine];
		currentLine ++;
	}
}