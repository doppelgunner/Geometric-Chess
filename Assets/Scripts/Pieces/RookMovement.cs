using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovement : Movement, IPieceMovement {

	public RookMovement(GCPlayer player, Piece piece) : base(player,piece) {
		BoundComputations += ComputeBound;
	}

	public override void ComputeBound() {
		Node currNode = piece.Node;
		int origRow = currNode.row;
		int origCol = currNode.col;
	
		Grid grid = GameManager.Instance.Grid;
		//up
		for (int up = 1; up + origRow < grid.Rows; up++) {
			int newRow = up + origRow;
			Node newNode = grid.GetNodeAt(newRow, origCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}

		//left
		for (int left = -1; left + origCol >= 0; left--) {
			int newCol = left + origCol;
			Node newNode = grid.GetNodeAt(origRow, newCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}

		//right
		for (int right = 1; right + origCol < grid.Cols; right++) {
			int newCol = right + origCol;
			Node newNode = grid.GetNodeAt(origRow, newCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}

		//down
		for (int bot = -1; bot + origRow >= 0; bot--) {
			int newRow = bot + origRow;
			Node newNode = grid.GetNodeAt(newRow, origCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}
	}
}
