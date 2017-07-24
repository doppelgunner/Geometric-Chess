using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleValue : MonoBehaviour {

	public Toggle toggle;
	public Text toggleText;

	private bool init  = false;

	void Awake() {
		int value = PlayerPrefs.GetInt(GameManager.CAMERA_VIEW,0);
		toggle.isOn = (value == 1) ? true : false;
		init = true;
	}

	public void OnValueChanged() {
		bool value = toggle.isOn;
		PlayerPrefs.SetInt(GameManager.CAMERA_VIEW,(value) ? 1 : 0);
	}
}
