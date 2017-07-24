using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AngleType {
	CUSTOM,
	_3D,
	_2D,
}

public class SwitchAngle : MonoBehaviour {

	public AngleType angleType; //using prefs manager to 2d or 3d TODO

	[SerializeField]
	private Vector3 p1CameraPos = new Vector3(-0.75f, 5.82f, -6.01f);
	[SerializeField]
	private Vector3 p1CameraRot = new Vector3(49.04f, 4.72f, 0);

	[SerializeField]
	private Vector3 p2CameraPos = new Vector3(0.75f, 5.82f, 6.01f);
	[SerializeField]
	private Vector3 p2CameraRot = new Vector3(49.04f, 184.72f, 0);


	private Vector3 p1CameraPos3D = new Vector3(-0.75f, 5.82f, -6.01f);
	private Vector3 p1CameraRot3D = new Vector3(49.04f, 4.72f, 0);
	private Vector3 p2CameraPos3D = new Vector3(0.75f, 5.82f, 6.01f);
	private Vector3 p2CameraRot3D = new Vector3(49.04f, 184.72f, 0);

	private Vector3 p1CameraPos2D = new Vector3(-0.75f, 8, 0f);
	private Vector3 p1CameraRot2D = new Vector3(90f, 0, 0);
	private Vector3 p2CameraPos2D = new Vector3(-0.75f, 8, 0f);
	private Vector3 p2CameraRot2D = new Vector3(90f, 180f, 0);

	private Camera rotateCamera;

	void LoadAngleType() {
		switch (angleType) {
			case AngleType._3D:
				p1CameraPos = p1CameraPos3D;
				p1CameraRot = p1CameraRot3D;
				p2CameraPos = p2CameraPos3D;
				p2CameraRot = p2CameraRot3D;
			break;
			case AngleType._2D:
				p1CameraPos = p1CameraPos2D;
				p1CameraRot = p1CameraRot2D;
				p2CameraPos = p2CameraPos2D;
				p2CameraRot = p2CameraRot2D;
			break;
		}

		SetCameraPosRot(p1CameraPos,p1CameraRot);
	}

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		int cameraView = PlayerPrefs.GetInt(GameManager.CAMERA_VIEW,0);
		angleType = (cameraView == 1) ? AngleType._3D : AngleType._2D;
		rotateCamera = GetComponent<Camera>();
		LoadAngleType();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchCamera(PlayerType playerType) {
		switch (playerType) {
			case PlayerType.P1:
				SetCameraPosRot(p1CameraPos, p1CameraRot);
				break;
			case PlayerType.P2:
				SetCameraPosRot(p2CameraPos, p2CameraRot);
				break;
		}
	}

	private void SetCameraPosRot(Vector3 pos, Vector3 rot) {
		rotateCamera.transform.position = pos;
		rotateCamera.transform.eulerAngles = rot;
	}
}
