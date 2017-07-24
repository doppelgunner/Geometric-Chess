using UnityEngine;
using System.Collections;
using System;

public class Scalable : MonoBehaviour {

	protected bool ready = false;

	protected Color origColor;
	protected Color origEmission;
	protected Material origMaterial;
	protected new Renderer renderer;

	protected virtual void Start() {
		renderer = GetComponent<Renderer>();
		origMaterial = GetComponent<Renderer>().material;
		origColor = origMaterial.GetColor("_Color");
		origEmission = origMaterial.GetColor("_EmissionColor");
	}

	public bool IsReady {
		get {
			return ready;
		}

		set {
			ready = value;
		}
	}

	public void SetMaterialOriginal() {
		SetMaterial(origMaterial);
	}

	public void SetEmissionOriginal() {
		SetEmission(origEmission);
	}

	public void SetColorOriginal() {
		SetColor(origColor);
	}

	public void SetMaterial(Material material) {
		renderer.material = material;
	}

	public void SetEmission(Color color) {
		renderer.material.SetColor("_EmissionColor", color);
		//DynamicGI.UpdateMaterials(r);
        //DynamicGI.UpdateEnvironment();
	}

	public void SetColor(Color color) {
		renderer.material.SetColor("_Color", color);
	}

	public void ScaleIn(float startAfter, float speed, float endScale) {
		StartCoroutine(IEScaleIn(startAfter,speed,endScale));
	}

	public void ScaleIn(float startAfter, float speed, Vector3 endScales) {
		StartCoroutine(IEScaleIn(startAfter,speed,endScales));
	}

	public void ScaleBy(float startAfter, float speed, float scaleBy) {
		StartCoroutine(IEScaleBy(startAfter,speed,scaleBy));
	}

	public void ScaleOut(float startAfter, float speed) {
		StartCoroutine(IEScaleOut(startAfter, speed));
	}

	public void ScaleOut(float startAfter, float speed, Action callback) {
		StartCoroutine(IEScaleOut(startAfter, speed,callback));	
	}

	public IEnumerator IEScaleBy(float startAfter, float speed, float scaleBy) {
		yield return new WaitForSeconds(startAfter);

		float t = Time.deltaTime * speed;
		float scale = 1f;
		float diffScale = scaleBy - scale;
		Vector3 origScale = gameObject.transform.localScale;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			gameObject.transform.localScale = origScale * (scale + diffScale * Mathf.Sin(t));
			yield return null;
		}
		gameObject.transform.localScale = origScale * scaleBy;
	}

	public IEnumerator IEScaleIn(float startAfter, float speed, float endScale) {
		yield return new WaitForSeconds(startAfter);
		ready = false;
		float t = Time.deltaTime * speed;
		float scale = 1f;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			scale = endScale * Mathf.Sin(t);
			transform.localScale = new Vector3(scale,scale,scale);
			yield return null;
		}
		transform.localScale = new Vector3(endScale, endScale, endScale);
		ready = true;
	}

	public IEnumerator IEScaleIn(float startAfter, float speed, Vector3 endScales) {
		yield return new WaitForSeconds(startAfter);
		ready = false;
		float t = Time.deltaTime * speed;
		float scale = 1f;
		while(t < Mathf.PI / 2) {
			t += Time.deltaTime * speed;
			scale = 1f * Mathf.Sin(t);
			transform.localScale = endScales * scale;
			yield return null;
		}
		transform.localScale = endScales * scale;
		ready = true;
	}

	public IEnumerator IEScaleOut(float startAfter, float speed) {
		yield return IEScaleOutBase(startAfter, speed); 
		Destroy(gameObject);
	}

	public IEnumerator IEScaleOut(float startAfter, float speed, Action callback) {
		yield return IEScaleOutBase(startAfter,speed);
		callback();
		Destroy(gameObject);
	}

	IEnumerator IEScaleOutBase(float startAfter, float speed) {
		yield return new WaitForSeconds(startAfter);

		float t = Time.deltaTime * speed;
		float scale = 1f;
		Vector3 origScale = gameObject.transform.localScale;
		t = Mathf.PI / 2f;
		while(t < Mathf.PI) {
			t += Time.deltaTime * speed;
			scale = 1 * Mathf.Sin(t);
			gameObject.transform.localScale = origScale * scale;
			yield return null;
		}
	}
}
