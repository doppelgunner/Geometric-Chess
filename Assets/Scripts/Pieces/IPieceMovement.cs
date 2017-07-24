using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ComputeBound();

public interface IPieceMovement {

	event ComputeBound BoundComputations;
	void ComputeBound();
	void Compute();
	void Moved();
	bool IsMoved();
}
