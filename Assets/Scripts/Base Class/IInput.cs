using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputReceiver {

	void EnableInput();
	void DisableInput();
	void OnInputEvent(InputActionType action);
}
