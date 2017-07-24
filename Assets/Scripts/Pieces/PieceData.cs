using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieceData {

	public GridCoords coords;
	public PieceType pieceType;
	public PlayerType playerType;

	public override string ToString() {
		return "Piece: \n"
		 + "\t" + coords.ToString() 
		 + "\n\tPieceType: " + pieceType
		 + "\n\tPlayerType: " + playerType.ToString();  

	}
}
