using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueTimer : MonoBehaviour {

	public Text sliderText;
	public Slider slider;

	private bool init = false;

	void Awake() {
		int oldTimer = PlayerPrefs.GetInt(GameManager.PLAYER_TIMER,GameManager.DEFAULT_PLAYER_TIMER_MIN);
		sliderText.text = oldTimer.ToString();
		slider.value = oldTimer;
		init = true;
	}

	public void OnValueChanged() {
		int value = ((int)slider.value);
		sliderText.text = value.ToString();

		PlayerPrefs.SetInt(GameManager.PLAYER_TIMER, value);
	}
}
