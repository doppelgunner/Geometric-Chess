using UnityEngine;
using System.Collections;
using System;

public class Scaler : MonoBehaviour {

	public IEnumerator IEScaleBy(float startAfter, GameObject go, float speed, float scaleBy) {
		yield return new WaitForSeconds(startAfter);

		float t = Time.deltaTime * speed;
		float scale = 1f;
		float diffScale = scaleBy - scale;
		Vector3 origScale = go.transform.localScale;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			go.transform.localScale = origScale * (scale + diffScale * Mathf.Sin(t));
			yield return null;
		}
		go.transform.localScale = origScale * scaleBy;
	}

	public IEnumerator IEScaleIn(float startAfter, GameObject go, float speed, float endScale) {
	yield return new WaitForSeconds(startAfter);
		go.tag = "Busy";
		float t = Time.deltaTime * speed;
		float scale = 1f;
		Vector3 origScale = go.transform.localScale;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			scale = endScale * Mathf.Sin(t);
			go.transform.localScale = new Vector3(scale,scale,scale);
			yield return null;
		}
		go.transform.localScale = new Vector3(endScale, endScale, endScale);
		go.tag = "Ready";
	}

	public IEnumerator IEScaleOut(float startAfter, GameObject go, float speed) {
		yield return IEScaleOutBase(startAfter, go, speed); 
		Destroy(go);
	}

	public IEnumerator IEScaleOut(float startAfter, GameObject go, float speed, Action callback) {
		yield return IEScaleOutBase(startAfter,go,speed);
		callback();
		Destroy(go);
	}

	IEnumerator IEScaleOutBase(float startAfter, GameObject go, float speed) {
		yield return new WaitForSeconds(startAfter);

		float t = Time.deltaTime * speed;
		float scale = 1f;
		Vector3 origScale = go.transform.localScale;
		t = Mathf.PI / 2f;
		while(t < Mathf.PI) {
			t += Time.deltaTime * speed;
			scale = 1 * Mathf.Sin(t);
			go.transform.localScale = origScale * scale;
			yield return null;
		}
	}
}
