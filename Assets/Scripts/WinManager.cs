using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour {

	public Text whiteScoreText;
	public Text blackScoreText;
	public Text winnerText;
	public Text maxGamesText;

	void Awake() {
		LoadScores();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadScores() {
		int white = PlayerPrefs.GetInt(GameManager.PLAYER_WHITE,0);
		int black = PlayerPrefs.GetInt(GameManager.PLAYER_BLACK,0);
		whiteScoreText.text = "White: " + white.ToString();
		blackScoreText.text = "Black: " + black.ToString();

		if (white > black) {
			winnerText.text = "WHITE";
		} else if (white < black) {
			winnerText.text = "BLACK";
		} else {
			winnerText.text = "NO ONE";
		}
		int maxGames = PlayerPrefs.GetInt(GameManager.GAME_MAX,0);
		maxGamesText.text = "OUT OF " + maxGames + " GAMES";
		GameManager.ResetScores();
	}
}
