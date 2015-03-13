using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	
	public float timeLeft;
	public Text timerText;
	public Text scoreText;
	public int fruitValue;
	public int score;
	public GameController gameCntrl;

	void Start () {
		score = 0;
		updateScore ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "bomb") {
			score = 0;
		}else if (collider.gameObject.tag == "et") {
			gameCntrl.addExtraTime();
		}else if (collider.gameObject.tag == "dt") {
			gameCntrl.deductExtraTime();
		}
		else if (collider.gameObject.tag == "fruit") {
			score += fruitValue;
		}
		updateScore ();
	}
	void updateScore(){
		scoreText.text = "Score:\n" + score.ToString();
		gameCntrl.score = score;
	}
}
