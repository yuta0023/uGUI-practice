using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour 
{
	public string[] scenarios;
	[SerializeField] Text uiText;
	
	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;	// 1文字の表示にかかる時間
	
	private int currentLine = 0;
	private string currentText = string.Empty;	// 現在の文字列
	private float timeUntilDisplay = 0;		// 表示にかかる時間
	private float timeElapsed = 1;			// 文字列の表示を開始した時間
	private int lastUpdateCharacter = -1;		// 表示中の文字数


	
	[SerializeField]
	private GameObject character;
	private Animator anim;
	private Image costume;
	[SerializeField]
	private Sprite changeCostume;
	private bool isChange = false;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText 
	{
		get { return  Time.time > timeElapsed + timeUntilDisplay; }
	}
	
	void Start()
	{
		anim = character.GetComponent<Animator> ();
		costume = character.GetComponent<Image>();
		SetNextLine();
	}
	
	void Update () 
	{
		if (!isChange) {
			if (currentText == "----------!!!") {
				isChange = true;
				StartCoroutine(ischangeCostume());
				Debug.Log ("change");
			}
		}
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if( IsCompleteDisplayText ){
			if(currentLine < scenarios.Length && Input.GetMouseButtonDown(0)){
				SetNextLine();
			}
		}else{
			// 完了してないなら文字をすべて表示する
			if(Input.GetMouseButtonDown(0)){
				timeUntilDisplay = 0;
			}
		}
		
		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if( displayCharacterCount != lastUpdateCharacter ){
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}

	IEnumerator ischangeCostume(){
		anim.SetBool("isChange",true);
		yield return new WaitForSeconds (1.5f);
		costume.sprite = changeCostume;
	}
	
	
	void SetNextLine()
	{
		currentText = scenarios[currentLine];
		currentLine ++;
		
		// 想定表示時間と現在の時刻をキャッシュ
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		
		// 文字カウントを初期化
		lastUpdateCharacter = -1;
	}
}