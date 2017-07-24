using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueScore : MonoBehaviour {

	public Text sliderText;
	public Slider slider;

	private bool init = false;

	void Awake() {
		int oldScore = PlayerPrefs.GetInt(GameManager.SCORE_MAX,1);
		sliderText.text = oldScore.ToString();
		slider.value = oldScore;
		init = true;
	}

	public void OnValueChanged() {
		int value = ((int)slider.value);
		sliderText.text = value.ToString();

		PlayerPrefs.SetInt(GameManager.SCORE_MAX, value);

		int maxGame = value * 2 - 1;
		PlayerPrefs.SetInt(GameManager.GAME_MAX, maxGame);

		if (init) {
			GameManager.ResetScores();
		}
	}
}
