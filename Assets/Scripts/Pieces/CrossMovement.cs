using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossMovement : Movement, IPieceMovement {


	public CrossMovement(GCPlayer player, Piece piece) : base(player,piece) {
		BoundComputations += ComputeBound;
	}

	public override void ComputeBound() {
		Node currNode = piece.Node;
		int origRow = currNode.row;
		int origCol = currNode.col;

		Grid grid = GameManager.Instance.Grid;

		for (int row = -1; row <= 1; row++) {
			for (int col = -1; col <= 1; col++) {
				if (row == 0 && col == 0) continue;

				int newRow = origRow + row;
				int newCol = origCol + col;
				ComputeMoveOrEatPiece(grid.GetNodeAt(newRow, newCol));
			}
		}
	}

}
