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
	Tilemap tilemap;
	int iteration = 0;

	IDictionary<string, string> directions = new Dictionary<string, string>(){
		{"U", "D"},
		{"D", "U"},
		{"L", "R"},
		{"R", "L"}
	};

	public Tile PickNewTile(string direction)
	{
		string newDirection = directions[direction];

		Tile tile = rooms[Random.Range(0, rooms.Length)];

		while (!tile.name.Contains(newDirection))
		{
			tile = rooms[Random.Range(0, rooms.Length)];
		}

		return tile;
	}

	public void WalkTile(int x, int y)
	{
		iteration += 1;

		if (iteration > maxIterations) return;

		Tile tile = tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));

		foreach (char direction in tile.name)
		{
			string dirString = direction.ToString();
			Vector3Int newPos;
			Tile newTile;
			Debug.Log(dirString);

			switch (dirString)
			{
				case "U":
					newPos = new Vector3Int(x, y + 1, 0);
					break;
				case "D":
					newPos = new Vector3Int(x, y - 1, 0);
					break;
				case "R":
					newPos = new Vector3Int(x + 1, y, 0);
					break;
				case "L":
					newPos = new Vector3Int(x - 1, y, 0);
					break;
				default:
					newPos = new Vector3Int(x, y, 0);
					break;
			}

			if (tilemap.GetTile<Tile>(newPos) != null)
			{
				continue;
			}

			newTile = PickNewTile(dirString);

			tilemap.SetTile(newPos, newTile);

			WalkTile(newPos.x, newPos.y);
		}

		return;
	}

	public Tile GetTileFromString(string id)
	{
		string realTileName = "";

		if (id.Contains("R"))
		{
			realTileName += "R";
		}
		if (id.Contains("D"))
		{
			realTileName += "D";
		}
		if (id.Contains("L"))
		{
			realTileName += "L";
		}
		if (id.Contains("U"))
		{
			realTileName += "U";
		}

		return Resources.Load<Tile>("Sprites/Tiles/Palettes/Tests/" + realTileName);
	}

	public Tile GetRequiredTile(int x, int y)
	{
		string tileName = "";

		Tile tile = tilemap.GetTile<Tile>(new Vector3Int(x + 1, y, 0));
		if (tile != null && tile.name.Contains("L"))
		{
			tileName += "R";
		}
		tile = tilemap.GetTile<Tile>(new Vector3Int(x - 1, y, 0));
		if (tile != null && tile.name.Contains("R"))
		{
			tileName += "L";
		}
		tile = tilemap.GetTile<Tile>(new Vector3Int(x, y + 1, 0));
		if (tile != null && tile.name.Contains("D"))
		{
			tileName += "U";
		}
		tile = tilemap.GetTile<Tile>(new Vector3Int(x, y - 1, 0));
		if (tile != null && tile.name.Contains("U"))
		{
			tileName += "D";
		}

		return GetTileFromString(tileName);
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
					tilemap.SetTile(pos, GetRequiredTile(x, y));
				}
			}
		}
	}

	public void Start()
	{
		tilemap = GetComponent<Tilemap>();

		tilemap.SetTile(Vector3Int.zero, rooms[Random.Range(0, rooms.Length)]);

		WalkTile(0, 0);

		FinishMap();
	}
}