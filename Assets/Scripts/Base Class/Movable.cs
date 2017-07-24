using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movable : Scalable {

	[SerializeField]
	protected float speed = 2f;
	protected Coroutine moveCoroutine;

	protected override void Start() {
		base.Start();
	}

	public virtual void MoveToXZ(Node node, Action finishCallback) {
		StopMoveCoroutine();
		moveCoroutine = StartCoroutine(IEMoveToXZ(node, finishCallback));
	}

	public virtual void MoveBy(Vector3 addPos, Action finishCallback) {
		StopMoveCoroutine();
		moveCoroutine = StartCoroutine(IEMoveBy(addPos, finishCallback));
	}

	protected virtual void StopMoveCoroutine() {
		if (moveCoroutine != null) {
			StopCoroutine(moveCoroutine);
		}
	}

	protected virtual IEnumerator IEMoveBy(Vector3 addPos, Action finishCallback) {
		ready = false;
		Vector3 origPos = transform.position;
		Vector3 targPos = origPos + addPos;
		
		while (true) {
			if (IsNear(targPos)) {
				ready = true;
				if (finishCallback != null) finishCallback();
				yield break;
			}
			transform.position = Vector3.MoveTowards(transform.position,targPos,speed * Time.deltaTime);

			yield return null;
		}
	}

	protected virtual IEnumerator IEMoveToXZ(Node node, Action finishCallback) {
		ready = false;
		Vector3 origPos = transform.position;
		Vector3 targPos = node.transform.position;
		targPos.y = origPos.y;
		while (true) {
			if (IsNear(targPos)) {
				ready = true;
				if (finishCallback != null) finishCallback();
				yield break;
			}
			transform.position = Vector3.MoveTowards(transform.position,targPos,speed * Time.deltaTime);
			yield return null;
		}
	}

	protected virtual bool IsNear(Vector3 targPos) {
		return (transform.position - targPos).sqrMagnitude < .01f;
	}
}
