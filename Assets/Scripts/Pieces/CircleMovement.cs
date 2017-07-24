using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement :  Movement, IPieceMovement {

	public CircleMovement(GCPlayer player, Piece piece) : base(player,piece) {
		BoundComputations += ComputeBound;
	}

	public override void ComputeBound() {
		Node currNode = piece.Node;
		int origRow = currNode.row;
		int origCol = currNode.col;
		
		Node frontNode = null;
		Node leftEatNode = null;
		Node rightEatNode = null;

		Grid grid = GameManager.Instance.Grid;
		GCPlayer p1 = p1 = GameManager.Instance.P1;

		int toAdd = 0;
		if (p1.Has(piece)) {
			toAdd = 1;
		} else {
			toAdd = -1;
		}

		frontNode = grid.GetNodeAt(origRow + toAdd, origCol);
		leftEatNode = grid.GetNodeAt(origRow + toAdd, origCol - 1);
		rightEatNode = grid.GetNodeAt(origRow + toAdd, origCol + 1);

		ComputeMoveOrEatPiece(leftEatNode);
		ComputeMoveOrEatPiece(rightEatNode);
		ComputeMoveOrEatPiece(frontNode);
	}
}
