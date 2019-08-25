using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
	public GameObject		backBrick;

    public GameObject[] _characters;

    public const int 		rows = 10;
	public const int 		columns = 5;


	private GameManager gameScript;

	private Transform			brickHolder;
	private	GameObject[][] 		brickArray = new GameObject[rows][];

    [Range(10f, 800f)]
    public float        spawnSpeed = 10;
	private float       spawnTimer;
	private bool        boardOk;

	void Awake()
	{
		gameScript = GetComponent<GameManager>();

		brickHolder = gameObject.GetComponent<Transform>();
		spawnTimer = 1;
		boardOk = false;
        Setup();
		boardOk = true;
	}


    //setp level and tiles
	void Setup()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			brickArray[y] = new GameObject[columns];
			x = 0;
			while (x < columns)
			{
				brickArray[y][x] = Instantiate(backBrick, new Vector3 (x + brickHolder.position.x, y + brickHolder.position.y, 0f), Quaternion.identity, brickHolder);
				x++;
			}
			y++;
		}


	}






	private bool CheckSelections(int x, int y, int xPos, int yPos)
	{
		int 	xVec = x - xPos;
		int 	yVec = y - yPos;
		bool 	matchSucces = false;
		bool 	posSucces = false;
		string 	newTile;

		newTile = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileNameInside();
		if (0 <= yPos - 1 && yPos + 1 < rows)
		{
			if (newTile == brickArray[yPos + 1][xPos].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[yPos - 1][xPos].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		if (0 <= yPos - 2)
		{
			if (newTile == brickArray[yPos - 1][xPos].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[yPos - 2][xPos].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		if (yPos + 2 < rows)
		{
			if (newTile == brickArray[yPos + 1][xPos].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[yPos + 2][xPos].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}



		if (0 <= xPos - 1 && xPos + 1 < columns)
		{
			if (newTile == brickArray[yPos][xPos + 1].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[yPos][xPos - 1].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		if (0 <= xPos - 2)
		{
			if (newTile == brickArray[yPos][xPos - 1].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[yPos][xPos - 2].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		if (xPos + 2 < columns)
		{
			if (newTile == brickArray[yPos][xPos + 1].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[yPos][xPos + 2].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}

		newTile = brickArray[yPos][xPos].GetComponent<BackGroundBrick>().GetTileNameInside();
		if (0 <= y - 1 && y + 1 < rows)
		{
			if (newTile == brickArray[y + 1][x].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[y - 1][x].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
			
		}
		if (0 <= y - 2)
		{
			if (newTile == brickArray[y - 1][x].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[y - 2][x].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		if (y + 2 < rows)
		{
			if (newTile == brickArray[y + 1][x].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[y + 2][x].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}



		if (0 <= x - 1 && x + 1 < columns)
		{
			if (newTile == brickArray[y][x + 1].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[y][x - 1].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		if (0 <= x - 2)
		{
			if (newTile == brickArray[y][x - 1].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[y][x - 2].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		if (x + 2 < columns)
		{
			if (newTile == brickArray[y][x + 1].GetComponent<BackGroundBrick>().GetTileNameInside())
				if (newTile == brickArray[y][x + 2].GetComponent<BackGroundBrick>().GetTileNameInside())
					matchSucces = true;
		}
		

		if ((-1 <= xVec && xVec <= 1 && yVec == 0) || (-1 <= yVec && yVec <= 1 && xVec == 0))
			posSucces = true;

		if (posSucces == true && matchSucces == true)
			return true;
		return false;
	}

	public void SwapSelectedPositions()
	{
		int 	x = 0;
		int 	y = 0;
		int 	xPos = 0;
		int 	yPos = 0;
		bool	foundFirst = false;
		bool 	foundSecond = false;
		string 	selectedTile = "None";

		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				if (foundFirst == false)
				{
					GameObject tempTile = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
					if (tempTile == null)
					{
						x++;
						continue;
					}
					if (tempTile.GetComponent<Tile>().isSelected)
					{
						xPos = x;
						yPos = y;
						selectedTile = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileNameInside();
						foundFirst = true;
					}
				}
				else
				{
					GameObject tempTile = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
					if (tempTile == null)
					{
						x++;
						continue;
					}
					if (tempTile.GetComponent<Tile>().isSelected)
					{
						foundSecond = true;
						break;
					}
				}
				x++;
			}
			if (foundSecond == true)
				break;
			y++;
		}

		if (foundSecond == true && CheckSelections(x, y, xPos, yPos))
		{
				ResetSelection();
				
				GameObject TileFirst = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
				GameObject TileSecond = brickArray[yPos][xPos].GetComponent<BackGroundBrick>().GetTileInside();
				Vector3		firstPos = TileFirst.transform.position;
				Vector3 	secondPos = TileSecond.transform.position;
				

				Instantiate(TileSecond, firstPos, Quaternion.identity, brickHolder);
				Instantiate(TileFirst, secondPos, Quaternion.identity, brickHolder);

				Destroy(brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside());
				Destroy(brickArray[yPos][xPos].GetComponent<BackGroundBrick>().GetTileInside());

				return ;
		}

		ResetSelection();
		return ;
	}

	void 	ResetSelection()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				GameObject 	tempTile = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileInside();
				if (tempTile != null)
				{
					tempTile.GetComponent<Tile>().isSelected = false;
					tempTile.GetComponent<Tile>().isChanging = true;
				}
				x++;
			}
			y++;
		}
		gameScript.tilesClicked = 0;
	}

	public void  SpawnTile(Vector3  blockPosition)
	{
		
		GameObject 	ClonedTile;
		int         rand;

		rand = Random.Range(0, 6);

		ClonedTile = Instantiate(_characters[rand], new Vector3 (blockPosition.x, blockPosition.y + 1, -5f), Quaternion.identity, brickHolder);
		ClonedTile.GetComponent<Tile>().gameScript = gameScript;

	}

	public int SpawnLine()
	{
		int x;
		int y;
		int spawned;

		spawned = 0;
		y = rows - 1;
		x = 0;
		while (x < columns)
		{
			if (!brickArray[y][x].GetComponent<BackGroundBrick>().SomethingInside())
			{
				SpawnTile(brickArray[y][x].transform.position);
				spawned++;
			}
			x++;
		}

		return spawned;
	}


	public bool CheckTurn()
	{
		int x;
		int y;

		int spaces = 0;
		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				if(!brickArray[y][x].GetComponent<BackGroundBrick>().SomethingInside())
					spaces++;
				x++;
			}
			y++;
		}

		if (spaces == 0)
		{
			return true;
		}
		return false;
	}

	public int 	RemoveHorizontalTiles()
	{
		int x = 0;
		int y = 0;
		int xPos = 0;
		int TilesOnLine = 0;
		int TilesFound = 0;
		string 	TileNow;

		y = 0;
		while (y < rows)
		{
			x = 0;
			TileNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileNameInside();
			while (x < columns)
			{
				xPos = x + 1;
				TilesOnLine = 1;
				TileNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileNameInside();
				if (TileNow == "None")
				{
					x++;
					continue;
				}
				while (xPos < columns)
				{
					if (TileNow == brickArray[y][xPos].GetComponent<BackGroundBrick>().GetTileNameInside())
						TilesOnLine++;
					else
						break;
					xPos++;
				}
				if (TilesOnLine >= 3)
				{
					TilesFound += TilesOnLine;
					xPos = x;
					while (TilesOnLine > 0)
					{
						brickArray[y][xPos].GetComponent<BackGroundBrick>().GetTileInside().GetComponent<Tile>().shouldDie = true;
						TilesOnLine--;
						xPos++;
					}
					gameScript.playersTurn = false;
					gameScript.stablePosition = 0;
					ResetSelection();
				}
				x++;
			}
			y++;
		}
		return TilesFound;
	}

	public int		RemoveVerticalTiles()
	{
		int x = 0;
		int y = 0;
		int yPos = 0;
		int TilesOnLine = 0;
		int TilesFound = 0;
		string 	TileNow;

		x = 0;
		while (x < columns)
		{
			y = 0;
			TileNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileNameInside();
			while (y < rows)
			{
				yPos = y + 1;
				TilesOnLine = 1;
				TileNow = brickArray[y][x].GetComponent<BackGroundBrick>().GetTileNameInside();
				if (TileNow == "None")
				{
					y++;
					continue;
				}
				while (yPos < rows)
				{
					if (TileNow == brickArray[yPos][x].GetComponent<BackGroundBrick>().GetTileNameInside())
						TilesOnLine++;
					else
						break;
					yPos++;
				}
				if (TilesOnLine >= 3)
				{
					TilesFound += TilesOnLine;
					yPos = y;
					while (TilesOnLine > 0)
					{
						brickArray[yPos][x].GetComponent<BackGroundBrick>().GetTileInside().GetComponent<Tile>().shouldDie = true;
						TilesOnLine--;
						yPos++;
					}
					gameScript.playersTurn = false;
					gameScript.stablePosition = 0;
					ResetSelection();
				}
				y++;
			}
			x++;
		}
		return TilesFound;
	}

	void Update()
	{
		if (spawnTimer > 2 && boardOk == true)
		{
			spawnTimer = 0;
			SpawnLine();
		}
		else
			spawnTimer += spawnSpeed * Time.deltaTime;
	}
}
