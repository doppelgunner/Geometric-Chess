using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : Movement, IPieceMovement {

	public KnightMovement(GCPlayer player, Piece piece) : base(player,piece) {
		BoundComputations += ComputeBound;
	}

	public override void ComputeBound() {
		Node currNode = piece.Node;
		int origRow = currNode.row;
		int origCol = currNode.col;

		Grid grid = GameManager.Instance.Grid;

		for (int row = -2; row <= 2; row++) {
			if (row == 0) continue;
			int col = GetCol(row, true);
			int newRow = origRow + row;
			int newCol = origCol + col;
			Node newNode = grid.GetNodeAt(newRow,newCol);
			ComputeMoveOrEatPiece(newNode);
		}

		for (int row = -2; row <= 2; row++) {
			if (row == 0) continue;
			int col = GetCol(row, false);
			int newRow = origRow + row;
			int newCol = origCol + col;
			Node newNode = grid.GetNodeAt(newRow,newCol);
			ComputeMoveOrEatPiece(newNode);
		}

	}

	private int GetCol(int n, bool posSign) {
		if (Mathf.Abs(n) == 2) return 1 * ((posSign) ? 1 : -1);
		if (Mathf.Abs(n) == 1) return 2 * ((posSign) ? 1 : -1);
		return 0;
	}
}
