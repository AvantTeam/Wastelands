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

	public void Start()
	{
		tilemap = GetComponent<Tilemap>();

		tilemap.SetTile(Vector3Int.zero, rooms[Random.Range(0, rooms.Length)]);

		WalkTile(0, 0);
	}
}