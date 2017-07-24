using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator {

	public static IPieceMovement CreatePieceMovement(MovementType movementType, GCPlayer player, Piece piece) {
		switch (movementType) {
			case MovementType.KING:
				return new KingMovement(player, piece);
			case MovementType.PAWN:
				return new PawnMovement(player, piece);
			case MovementType.ROOK:
				return new RookMovement(player, piece);
			case MovementType.BISHOP:
				return new BishopMovement(player, piece);
			case MovementType.QUEEN:
				return new QueenMovement(player, piece);
			case MovementType.KNIGHT:
				return new KnightMovement(player, piece);
			case MovementType.CIRCLE:
				return new CircleMovement(player, piece);
			case MovementType.CROSS:
				return new CrossMovement(player, piece);
			case MovementType.NONE:
			default:
				return new NoMovement(player, piece);
		}
	}
}
