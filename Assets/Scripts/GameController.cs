using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public Camera mCamera;
	public GameObject[] objects;
	public float timeLeft;
	public float maxWidth;
	public Text timerText;
	public int score;
	public GameObject restartButton;
	public GameObject gameOverText;
	public Text highScoreText;
	public GameObject startGameButton;
	private bool playing;
	public basketController basketCntrl;
	// Use this for initialization
	void Start () {
		if (mCamera == null) {
			mCamera = Camera.main;
		}
		playing = false;
		//Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);;
		//Vector3 targetWidth = mCamera.ScreenToWorldPoint (upperCorner);
		//float fruitWidth = GetComponent<Renderer>().bounds.extents.x;
		//maxWidth = targetWidth.x - fruitWidth;
		highScoreText.text = "High Score: " +(PlayerPrefs.GetInt("High Score")).ToString();
		timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft).ToString();
	}

	public void addExtraTime(){
		if (timeLeft != 0) {
			timeLeft += 5;
			timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft).ToString ();
		}
	}
	public void deductExtraTime(){
		if (timeLeft != 0) {
			timeLeft -= 5;
			timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft).ToString ();
		}
	}

	void FixedUpdate(){
		if (playing) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft).ToString ();
		}
	}
	IEnumerator Spawn(){
		yield return new WaitForSeconds(2.0f);
		playing = true;
		while (timeLeft >0) {
			GameObject tempObj = objects[Random.Range(0,objects.Length)];
			if(timeLeft>=10){
				tempObj = objects[Random.Range(0,objects.Length-1)];
			}
			Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth,maxWidth),transform.position.y,0.0f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (tempObj,spawnPosition,spawnRotation);
			yield return new WaitForSeconds(Random.Range(0.0f,1.0f));
		}
		yield return new WaitForSeconds(2.0f);
		basketCntrl.toggleControl (false);
		int highScore = PlayerPrefs.GetInt ("High Score");
		if (score > highScore) { //when game over set highscore = to that score
			highScore = score;
			PlayerPrefs.SetInt ("High Score", highScore);
			highScoreText.gameObject.SetActive (true);
			highScoreText.text = "New High Score: " +(highScore).ToString();
		}
		gameOverText.SetActive (true);
		yield return new WaitForSeconds(1.0f);
		restartButton.SetActive (true);

	}
	public void StartGame(){
		startGameButton.SetActive (false);
		highScoreText.gameObject.SetActive (false);
		basketCntrl.toggleControl (true);
		StartCoroutine (Spawn ());
	}
}
