using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : Movement, IPieceMovement {

	public BishopMovement(GCPlayer player, Piece piece) : base(player,piece) {
		BoundComputations += ComputeBound;
	}

	public override void ComputeBound() {
		Node currNode = piece.Node;
		int origRow = currNode.row;
		int origCol = currNode.col;
		
		Grid grid = GameManager.Instance.Grid;

		//right-up
		for (int ru = 1; ru + origRow < grid.Rows && ru + origCol < grid.Cols; ru++) {
			int newRow = ru + origRow;
			int newCol = ru + origCol;
			Node newNode = grid.GetNodeAt(newRow, newCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}

		//left-up
		for (int lu = -1; origRow - lu < grid.Rows && lu + origCol >= 0; lu--) {
			int newRow = origRow - lu;
			int newCol = lu + origCol;
			Node newNode = grid.GetNodeAt(newRow, newCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}

		//right-bot
		for (int rb = 1; origRow - rb >= 0 && rb + origCol < grid.Cols; rb++) {
			int newRow = origRow - rb;
			int newCol = rb + origCol;
			Node newNode = grid.GetNodeAt(newRow, newCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}

		//left-bot
		for (int lb = -1; lb + origRow >= 0 && lb + origCol >= 0; lb--) {
			int newRow = lb + origRow;
			int newCol = lb + origCol;
			Node newNode = grid.GetNodeAt(newRow, newCol);
			if (ComputeMoveOrEatPieceEnemyAlly(newNode)) break;
		}
	}
	
}
