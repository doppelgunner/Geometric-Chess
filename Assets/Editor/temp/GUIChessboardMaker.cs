using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIChessboardMaker : MonoBehaviour {

    private Grid grid;
    public GridCoords coords;
    
    void Awake() {
        /*
        PlayerPrefs.DeleteAll();
        
        PieceData pData = new PieceData();
        pData.coords = new GridCoords(1,1);    
        pData.pieceType = PieceType.CIRCLE;
        pData.playerType = PlayerType.P2;  

        FileHelper.Serialize("pawn1", pData);
        PieceData pData2 = FileHelper.Deserialize<PieceData>("pawn1");
        Debug.Log(pData2);
        */
    }
}
