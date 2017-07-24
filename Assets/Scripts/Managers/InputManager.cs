using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputActionType {
	GRAB_PIECE = 0,
	PLACE_PIECE = 1,
	CANCEL_PIECE = 2,
	ZOOM_IN = 3,
	ZOOM_OUT = 4,
	ROTATE = 5,
	STOP_ROTATE = 6,
}

public class InputManager : Singleton<InputManager> {
	
	public delegate void InputEventHandler(InputActionType actionType);
	public static event InputEventHandler InputEvent;

	private bool clicked;
	private Node currentNode;
	private GCPlayer currentPlayer;

	public Vector2 mouseAxis;

	public Vector2 MouseAxis {
		get {return mouseAxis;}
	}

	void Awake() {
		_destroyOnLoad = destroyOnLoad;
		mouseAxis = new Vector2(0,0);
	}

	void OnDisable() {
		InputEvent = null;
	}

	void Update() {
		mouseAxis.x = Input.GetAxis("Mouse X");
		mouseAxis.y = Input.GetAxis("Mouse Y");

		if (InputEvent == null) return;

		if (!GameManager.Instance.IsReady) return;

		HighlightTile();

		if (Input.GetMouseButtonUp(0)) {
			if (GameManager.Instance.GameState.IsWaiting) {
				UnHighlightTile();
				InputEvent(InputActionType.GRAB_PIECE);
			} else if (GameManager.Instance.GameState.IsHolding) {
				InputEvent(InputActionType.PLACE_PIECE);
			}
		}

		if (Input.GetMouseButtonUp(1)) {
			if (GameManager.Instance.GameState.IsHolding) {
				InputEvent(InputActionType.CANCEL_PIECE);
			}
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			InputEvent(InputActionType.ZOOM_IN);
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			InputEvent(InputActionType.ZOOM_OUT);
		}

		if (Input.GetMouseButtonDown(2)) {
			InputEvent(InputActionType.ROTATE);
		} else if (Input.GetMouseButtonUp(2)) {
			InputEvent(InputActionType.STOP_ROTATE);
		}
	}

	public void HighlightTile() {
		if (GameManager.Instance.GameState.IsWaiting) {
			UnHighlightTile();
			currentNode = Finder.RayHitFromScreen<Node>(Input.mousePosition);
			if (currentNode != null) {
				Piece piece = currentNode.Piece;
				if (piece != null) {
					if (GameManager.Instance.CurrentPlayer.Has(piece)) {
						currentNode.HighlightMove();
					} else {
						currentNode.HighlightEat();
					}
				}
			}
		}
	}

	public void UnHighlightTile() {
		if (currentNode != null) {
			currentNode.UnhighlightEat();
			currentNode.UnhighlightMove();
		}
	}

}
