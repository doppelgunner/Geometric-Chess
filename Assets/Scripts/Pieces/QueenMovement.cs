using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : Movement, IPieceMovement {

	private IPieceMovement rook;
	private IPieceMovement bishop;

	public QueenMovement(GCPlayer player, Piece piece) : base(player,piece) {
		rook = new RookMovement(player, piece);
		bishop = new BishopMovement(player, piece);
		BoundComputations += rook.ComputeBound;
		BoundComputations += bishop.ComputeBound;
	}

	public override void ComputeBound() {
		//do nothing
	}

	
}
