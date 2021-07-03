using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MapGen : MonoBehaviour
{
	//only works with single tiles, AS OF NOW
	public Tile[] rooms;
	public int maxIterations = 16;
	public Vector2Int size = new Vector2Int(30, 30);
	public Tilemap tilemap;
	int iteration = 0;
	List<List<string>> roomsString = new List<List<string>>();
	Vector2Int center;

	IDictionary<string, string> directions = new Dictionary<string, string>(){
		{"U", "D"},
		{"D", "U"},
		{"L", "R"},
		{"R", "L"}
	};

	public string PickNewTile(string direction)
	{
		string newDirection = directions[direction];

		string tile = rooms[Random.Range(0, rooms.Length)].name;

		while (!tile.Contains(newDirection))
		{
			tile = rooms[Random.Range(0, rooms.Length)].name;
		}

		return tile;
	}

	public void WalkTile(int x, int y)
	{
		string tile = roomsString[y][x];

		foreach (char direction in tile)
		{
			string dirString = direction.ToString();
			Vector2Int newPos;
			string newTile;

			switch (dirString)
			{
				case "U":
					newPos = new Vector2Int(x, y + 1);
					break;
				case "D":
					newPos = new Vector2Int(x, y - 1);
					break;
				case "R":
					newPos = new Vector2Int(x + 1, y);
					break;
				case "L":
					newPos = new Vector2Int(x - 1, y);
					break;
				default:
					newPos = new Vector2Int(x, y);
					break;
			}

			if (newPos.x < 0 || newPos.x >= size.x || newPos.y < 0 || newPos.y >= size.y)
			{
				continue;
			}

			newTile = PickNewTile(dirString);
			Debug.Log(newTile.Contains(directions[dirString]));
			roomsString[newPos.y][newPos.x] = newTile;

			iteration += 1;
			if (iteration > maxIterations) return;

			WalkTile(newPos.x, newPos.y);
		}

		return;
	}
	public string GetRequiredTile(int x, int y)
	{
		string tileName = "";

		string tile = roomsString[y][x + 1];
		if (tile != null && tile.Contains("L"))
		{
			tileName += "R";
		}
		tile = roomsString[y][x - 1];
		if (tile != null && tile.Contains("R"))
		{
			tileName += "L";
		}
		tile = tile = roomsString[y - 1][x];
		if (tile != null && tile.Contains("D"))
		{
			tileName += "U";
		}
		tile = roomsString[y + 1][x];
		if (tile != null && tile.Contains("U"))
		{
			tileName += "D";
		}

		return tileName;
	}

	public void FinishMap()
	{
		BoundsInt bounds = tilemap.cellBounds;
		Vector3Int pos;

		for (int x = bounds.min.x; x < bounds.max.x; x++)
		{
			for (int y = bounds.min.y; y < bounds.max.y; y++)
			{
				pos = new Vector3Int(x, y, 0);

				Tile tile = tilemap.GetTile<Tile>(pos);

				if (tile != null)
				{
					Debug.Log("d");
					//tilemap.SetTile(pos, GetRequiredTile(x, y));
				}
			}
		}
	}

	public void Start()
	{
		for (int i = 0; i < size.x; i++)
		{
			roomsString.Add(new List<string>());
			for (int j = 0; j < size.y; j++)
			{
				roomsString[i].Add("");
			}
		}

		center = new Vector2Int((int)Mathf.Floor(size.x / 2) - 1, (int)Mathf.Floor(size.y / 2) - 1);

		roomsString[center.y][center.x] = rooms[Random.Range(0, rooms.Length)].name;

		WalkTile(center.x, center.y);

		int xx = -1;
		int yy = -1;
		Vector3Int pos = new Vector3Int(0, 0, 0);
		string o = "";
		foreach (List<string> item in roomsString)
		{
			o += "\n";
			yy++;
			xx = -1;
			foreach (string i in item)
			{
				o += i + ".";
				xx++;
				if (i == "") continue;
				pos = new Vector3Int(yy, xx, 0);
				tilemap.SetTile(pos, Resources.Load<Tile>("Sprites/Tiles/Palettes/Tests/" + i));
			}
		}
		Debug.Log(o);


		//FinishMap();
	}
}