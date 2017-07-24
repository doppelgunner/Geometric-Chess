using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Grid : MonoBehaviour {

	[SerializeField]
	private GameObject tilePrefab;
	[SerializeField]
	private GameObject tilePrefab2;

	[SerializeField]
	private int rows;
	[SerializeField]
	private int cols;
	//[SerializeField]
	//private int height;
	[SerializeField]
	private bool checkDiagonals = true;

	[SerializeField]
	private GameObject[] piecesPrefabs;

	private Node[,] grid;
	private Vector3 tileSize;
	private Vector3 size;

	private bool piecesSpawned;

	public int Rows {
		get {return rows;}
	}

	public int Cols {
		get {return cols;}
	}

	public bool ArePiecesSpawned {
		get {
			return piecesSpawned;
		}
	}

	public bool IsReady {
		get{
			for (int row = 0; row < rows; row++) {
				for (int col = 0; col < cols; col++) {
					Node node = grid[row,col];
					if (!node.IsReady) return false;
				}
			}

			return true;
		}
	}

	public int Size {
		get {
			return rows * cols;
		}
	} 

	public Node GetNodeAt(int row, int col) {
		if (row < 0 || row >= rows || col < 0 || col >= cols) return null;
		return grid[row,col];
	}

	public Node GetNodeAt(Vector3 pos) {
		pos -= transform.position;

		float percentRow = (pos.z / size.z) + 0.5f;
		float percentCol = (pos.x / size.x) + 0.5f;
		percentRow = Mathf.Clamp01(percentRow);
		percentCol = Mathf.Clamp01(percentCol);
		int row = Mathf.RoundToInt((rows-1) * percentRow);
		int col = Mathf.RoundToInt((cols-1) * percentCol);

		return grid[row,col];
	}

	void Awake() {
		grid = new Node[rows,cols];
		tileSize =  tilePrefab.GetComponent<Renderer>().bounds.size;
		size = new Vector3(tileSize.x * cols, tileSize.y, tileSize.z * rows);
		CreateGrid();
	}

	void CreateGrid() {
		Vector3 bottomLeft = new Vector3(
				transform.position.x - size.x / 2 + tileSize.x / 2,
				transform.position.y, 
				transform.position.z - size.z / 2 + tileSize.z / 2);
		Vector3 startPosition = bottomLeft;

		GameObject tile = tilePrefab;

		for (int row = 0; row < rows; row++) {
			for (int col = 0; col < cols; col++) {
				startPosition.z = bottomLeft.z + tileSize.z * row;
				startPosition.x = bottomLeft.x + tileSize.x * col;
				GameObject go = Instantiate(tile, startPosition, tile.transform.rotation) as GameObject;
				Node dn = go.AddComponent<Node>();
				dn.row = row;
				dn.col = col;
				dn.rowChess = Converter.ToChessRow(row);
				dn.colChess = Converter.ToChessCol(col);
				grid[row,col] = dn;
				go.transform.parent = transform;
				go.transform.localScale = Vector3.zero;

				dn.ScaleIn(Random.Range(0f,1f),Random.Range(1f,2f),tile.transform.localScale);
				tile = SwapTilePrefab(tile);
			}
			tile = SwapTilePrefab(tile);
		}

		StartCoroutine(SpawnPieces());
	}

	GameObject SwapTilePrefab(GameObject go) {
		if (tilePrefab == go) return tilePrefab2;
		
		return tilePrefab;
	}

	IEnumerator SpawnPieces() {
		while (!IsReady) {
			yield return null;
		}

		//0 - box
		//1 - triangle
		//2 - circle
		//3 - cross
		//4 - hexagon

		PlayerType p1T = PlayerType.P1;
		PlayerType p2T = PlayerType.P2;

		///*----------------------CHESS sample
		//spawn circles
		for (int i = 0; i <= 7; i++) {
			SpawnPiece(new GridCoords(1,i),piecesPrefabs[2], p1T); // p1 circ
			SpawnPiece(new GridCoords(6,i),piecesPrefabs[2], p2T); // p2 circ
		}

		//spawn boxes
		SpawnPiece(new GridCoords(0,0),piecesPrefabs[0], p1T); //p1 box
		SpawnPiece(new GridCoords(0,7),piecesPrefabs[0], p1T); //p1 box
		SpawnPiece(new GridCoords(7,0),piecesPrefabs[0], p2T); //p2 box
		SpawnPiece(new GridCoords(7,7),piecesPrefabs[0], p2T); //p2 box

		//spawn triangles
		SpawnPiece(new GridCoords(0,2),piecesPrefabs[1], 180, p1T); //p1 tri
		SpawnPiece(new GridCoords(0,5),piecesPrefabs[1], 180, p1T); //p1 tri
		SpawnPiece(new GridCoords(7,2),piecesPrefabs[1], p2T); //p2 tri
		SpawnPiece(new GridCoords(7,5),piecesPrefabs[1], p2T); //p2 tri

		//spawn crosses
		SpawnPiece(new GridCoords(0,4),piecesPrefabs[3], p1T); //p1 cross
		SpawnPiece(new GridCoords(7,4),piecesPrefabs[3], p2T); //p2 cross

		//spawn hexagons
		SpawnPiece(new GridCoords(0,3),piecesPrefabs[4], p1T); //p1 hex
		SpawnPiece(new GridCoords(7,3),piecesPrefabs[4], p2T); //p2 hex

		//spawn rectangles - knights for testing
		SpawnPiece(new GridCoords(0,1),piecesPrefabs[5], p1T); //p1 rect
		SpawnPiece(new GridCoords(0,6),piecesPrefabs[5], p1T); //p1 rect
		SpawnPiece(new GridCoords(7,1),piecesPrefabs[5], p2T); //p2 rect
		SpawnPiece(new GridCoords(7,6),piecesPrefabs[5], p2T); //p2 rect
		//*/


		/*------------------GC
		for (int i = 1; i <= 6; i++) {
			SpawnPiece(new GridCoords(2,i),piecesPrefabs[2], p1T); // p1 circ
			SpawnPiece(new GridCoords(5,i),piecesPrefabs[2], p2T); // p2 circ
		}

		//spawn boxes
		SpawnPiece(new GridCoords(1,1),piecesPrefabs[0], p1T); //p1 box
		SpawnPiece(new GridCoords(1,6),piecesPrefabs[0], p1T); //p1 box
		SpawnPiece(new GridCoords(6,1),piecesPrefabs[0], p2T); //p2 box
		SpawnPiece(new GridCoords(6,6),piecesPrefabs[0], p2T); //p2 box

		//spawn triangles
		SpawnPiece(new GridCoords(1,2),piecesPrefabs[1], 180, p1T); //p1 tri
		SpawnPiece(new GridCoords(1,5),piecesPrefabs[1], 180, p1T); //p1 tri
		SpawnPiece(new GridCoords(6,2),piecesPrefabs[1], p2T); //p2 tri
		SpawnPiece(new GridCoords(6,5),piecesPrefabs[1], p2T); //p2 tri

		//spawn crosses
		SpawnPiece(new GridCoords(1,4),piecesPrefabs[3], p1T); //p1 cross
		SpawnPiece(new GridCoords(6,4),piecesPrefabs[3], p2T); //p2 cross

		//spawn hexagons
		SpawnPiece(new GridCoords(1,3),piecesPrefabs[4], p1T); //p1 hex
		SpawnPiece(new GridCoords(6,3),piecesPrefabs[4], p2T); //p2 hex

		*/

		piecesSpawned = true;
	}

	public void SpawnPiece(GridCoords coords, GameObject piece, float yRotation, PlayerType playerType) {
		Node pieceNode = GetNodeAt(coords.row, coords.col);

		Vector3 pRotation = piece.transform.rotation.eulerAngles;
		Quaternion newPRotation = Quaternion.Euler(pRotation.x, yRotation, pRotation.z);

		GameObject pieceObject = Instantiate(piece, pieceNode.transform.position + Vector3.up * 1.2f, newPRotation) as GameObject;
		pieceObject.transform.localScale = Vector3.zero; //for scaling in start from zero
		Piece pieceScript = pieceObject.GetComponent(typeof(Piece)) as Piece;

		//assign mat and player type
		Material mat = null;
		GCPlayer player = null;
		switch (playerType) {
			case PlayerType.P1:
				mat = GameManager.Instance.PieceP1;
				player = GameManager.Instance.P1;
				break;
			case PlayerType.P2:
				mat = GameManager.Instance.PieceP2;
				player = GameManager.Instance.P2;
				break;
		}
		pieceObject.GetComponent<Renderer>().material = mat;
		player.AddPieces(pieceScript);
		pieceScript.PieceMovement = Creator.CreatePieceMovement(pieceScript.MovementType, player, pieceScript);
		
		if(pieceScript) //if exists type then scale
			pieceScript.ScaleIn(Random.Range(0f,1f),Random.Range(1f,2f),piece.transform.localScale);
		

		pieceScript.UpdateNode(pieceNode);
	}

	public void SpawnPiece(GridCoords coords, GameObject piece, PlayerType playerType) {
		SpawnPiece(coords, piece, 0, playerType);
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();
		for (int row = -1; row <= 1; row++) {
			for (int col = -1; col <= 1; col++) {

				//skip these ones
				if (row == 0 && col == 0)
					continue;
				if (!checkDiagonals && (row * row) == 1 && (col * col) == 1)
					continue;

				int checkRow = node.row + row;
				int checkCol = node.col + col;

				if (checkRow >= 0 && checkRow < rows && checkCol >= 0 && checkCol < cols) {
					neighbours.Add(grid[checkRow,checkCol]);
				}
			}
		}

		return neighbours;
	}

	public bool IsNearby(Node nodeA, Node nodeB) {
		for (int row = -1; row <= 1; row++) {
			for (int col = -1; col <= 1; col++) {

				//skip these ones
				if (row == 0 && col == 0)
					continue;
				if (!checkDiagonals && (row * row) == 1 && (col * col) == 1)
					continue;

				int checkRow = nodeA.row + row;
				int checkCol = nodeA.col + col;

				if (checkRow >= 0 && checkRow < rows && checkCol >= 0 && checkCol < cols) {
					if (grid[checkRow,checkCol] == nodeB) return true;
				}
			}
		}

		return false;
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(2,2,2));
	}
}
