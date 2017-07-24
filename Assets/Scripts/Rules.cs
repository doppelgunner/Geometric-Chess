using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour {

	public static bool IsAlly(Piece piece1, Piece piece2) {
		GCPlayer p1 = GameManager.Instance.P1;
		if (p1.Has(piece1) && p1.Has(piece2)) return true;
		
		GCPlayer p2 = GameManager.Instance.P2;
		if (p2.Has(piece1) && p2.Has(piece2)) return true;

		return false;
	}

	public static bool IsEnemy(Piece piece1, Piece piece2) {
		GCPlayer p1 = GameManager.Instance.P1;
		if (p1.Has(piece1) && !p1.Has(piece2)) return true;
		if (p1.Has(piece2) && !p1.Has(piece1)) return true;
		return false;
	}

	public static bool CheckKing(GCPlayer player, Node checkedByNode, Node checkedNode) {
		if (checkedNode.Piece.PieceType == PieceType.CROSS) {
			GameManager.Instance.Opponent(player).CheckedBy = checkedByNode.Piece;
			//checkedPiece.Node.HighlightCheck(); //Experimental
			//checkedBy.Node.HighlightCheck(); //Experimental
			return true;
		}
		return false;
	}

	//Modifies the move if modify = true
	//not safe
	public static bool IsCheckMove(GCPlayer player, Piece piece, Node tNode, bool modify) {
		Node oldNode = piece.Node;
		piece.UpdateNode(tNode);
		Piece checkedBy = player.CheckedBy;
		player.ClearCheck();
		GameManager.Instance.Opponent(player).ComputePieces();
		if (player.IsChecked) {
			piece.UpdateNode(oldNode);
			player.CheckedBy = checkedBy;
			return true;
		}

		if (!modify) {
			piece.UpdateNode(oldNode);
			player.CheckedBy = checkedBy;
		}
		return false;
	}

	public static bool IsGuardedMove(GCPlayer player, Piece piece, Node tNode) {
		List<Piece> oppPieces = GameManager.Instance.Opponent(player).Pieces;
		for (int i = 0; i < oppPieces.Count; i++) {
			if (oppPieces[i].IsPossibleMove(tNode)) {
				return true;
			}
		} 

		return false;
	}


	//Modifies the move if modify = true;
	public static bool IsCheckEat(GCPlayer player, Piece piece, Node tNode, bool modify) {
		Node oldNode = piece.Node;
		Piece tPiece = tNode.Piece;
		tPiece.UpdateNode(null);
		piece.UpdateNode(tNode);
		Piece checkedBy = player.CheckedBy;
		player.ClearCheck();
		GameManager.Instance.Opponent(player).ComputePieces();
		if (player.IsChecked) {
			piece.UpdateNode(oldNode);
			tPiece.UpdateNode(tNode);
			player.CheckedBy = checkedBy;
			return true;
		}

		if (!modify) {
			piece.UpdateNode(oldNode);
			tPiece.UpdateNode(tNode);
			player.CheckedBy = checkedBy;
		}
		return false;
	}

	public static bool HasNoMove() {
		GCPlayer player = GameManager.Instance.CurrentPlayer;
		List<Piece> pieces = player.Pieces;

		for (int i = 0; i < pieces.Count; i++) {
			List<Node> possibleMoves = pieces[i].PossibleMoves;
			for (int j = 0; j < possibleMoves.Count; j++) {
				Node tNode = possibleMoves[j];
				if (!Rules.IsCheckMove(player, pieces[i], tNode, false)) {
					return false;
				}
			}
			
			List<Node> possibleEats = pieces[i].PossibleEats;
			for (int j = 0; j < possibleEats.Count; j++) {
				Node tNode = possibleEats[j];
				if (!Rules.IsCheckEat(player, pieces[i], tNode, false)) {
					return false;
				}
			}
		}

		return true;
	}
}
