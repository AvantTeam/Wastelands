using Content;
using Utilities;
using static Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MapGen : MonoBehaviour
{
	//only works with single tiles, AS OF NOW
	public Tile[] rooms;
	public int maxIterations = 16;
	public int minRooms = 10;
	Tilemap tilemap;
	int iteration = 0;

	int count = 0;

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

		if(id == "B" || id == "F" || id.StartsWith("s")){
			return null;
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

	public int CountTiles()
	{
		int amount = 0;

		BoundsInt bounds = tilemap.cellBounds;
		foreach (Vector3Int pos in bounds.allPositionsWithin)
		{
			Tile tile = tilemap.GetTile<Tile>(pos);
			if (tile != null)
			{
				amount += 1;
			}
		}

		return amount;
	}

	public string RoomString(){
		Vector3Int prevPos = default;
		string output = "";

		List<Vector3Int> invert = new List<Vector3Int>();
		BoundsInt bounds = tilemap.cellBounds;
		foreach (Vector3Int pos in bounds.allPositionsWithin)
		{
			invert.Add(pos);
		}

		invert.Reverse();

		foreach (Vector3Int pos in invert)
		{
			Tile tile = tilemap.GetTile<Tile>(pos);

			print(pos);

			if (tile != null) output += tile.name;
			else output += "B";

			if (prevPos == default) prevPos = pos;
			if (prevPos.y < pos.y) output += ";";
			else output += ".";

			prevPos = pos;
		}

		return output;
	}

	public List<List<string>> ListRoomFromString(string room){
		List<string> rows = SplitByChar(room, ';');
		List<List<string>> output = new List<List<string>>();

		foreach(string i in rows){
			List<string> columns = SplitByChar(i, '.');

			output.Add(columns);
		}

		return output;
	}

	public void LoadRoom(Vector3Int pos, List<List<string>> room){
		Vector3Int newPos = pos;

		int x = 0, y = 0;

		foreach(List<string> i in room){
			foreach(string j in i){
				tilemap.SetTile(newPos, GetTileFromString(j));

				newPos = new Vector3Int(x + pos.x, y + pos.y, 0);

				x++;
			}

			x = 0;
			y++;
		}
	}

	public void Start()
	{
		tilemap = GetComponent<Tilemap>();

		while (count < minRooms)
		{
			tilemap.ClearAllTiles();

			tilemap.SetTile(Vector3Int.zero, rooms[Random.Range(0, rooms.Length)]);

			WalkTile(0, 0);

			FinishMap();

			count = CountTiles();
		}

		string roomString = RoomString();

		List<List<string>> roomList = ListRoomFromString(roomString);

		RandomDictionary<List<List<string>>> roomDict = ContentLoader.rooms.roomDict;

		Vector3Int pos = Vector3Int.zero;
		foreach(List<string> i in roomList){
			foreach(string j in i){
				if(j == "B") continue;

				List<List<string>> room = roomDict.Get(j);

				LoadRoom(pos, room);

				pos.x += 25;
			}
			pos.x = 0;
			pos.y += 18;
		}

		tilemap.ClearAllTiles();
	}
}