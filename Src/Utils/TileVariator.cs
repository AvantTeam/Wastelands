using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileVariator : MonoBehaviour
{
	public string path;
	public string tileName;
	public int layer;
	public Tile originalTile;
	public Tile[] defaultTileShades;
	public float shadeWidth = 3f;
	public float shadeHeight = 3f;
	public float shades = 4f;
	[Range(0.2f, 5f)] public float variationRarity = 1f;
	[Range(0.2f, 5f)] public float shadeRarity = 1f;
	Tilemap tilemap;
	List<Tile> tileList = new List<Tile>();
	Tile[] tileArray;

	void Start()
	{
		float tiles = shadeHeight * shadeWidth * shades - 1;

		tilemap = GetComponent<Tilemap>();
		path = "Sprites/Tiles/Palettes/" + path + "/";

		for (int i = 0; i < tiles; i++)
		{
			Tile tileFound = Resources.Load<Tile>(path + tileName + "_" + (i + 1).ToString());
			tileList.Add(tileFound);
		}

		tileArray = tileList.ToArray();

		BoundsInt bounds = tilemap.cellBounds;
		TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
		float tileAmount = allTiles.Length;

		Vector3Int position = new Vector3Int();
		for (int x = tilemap.origin.x; x < (tilemap.origin.x + tilemap.size.x); x++)
		{
			for (int y = tilemap.origin.y; y < (tilemap.origin.y + tilemap.size.y); y++)
			{
				position.x = x;
				position.y = y;
				position.z = layer;

				Tile tile = tilemap.GetTile<Tile>(position);
				int index = 0;
				if (tile != null && tile == originalTile)
				{
					if (Random.Range(0f, variationRarity) <= 0.2f)
					{
						index = 0;
						while (index <= 0)
						{
							index =
								Random.Range(0, ((int)shadeWidth)) +
								Random.Range(0, ((int)shadeHeight)) * ((int)(shadeWidth * shades)) - 1;
						}

						if (Random.Range(0, shadeRarity) <= 0.2f) index += ((int)Mathf.Round(Mathf.PerlinNoise(x / 10f + 3000f, y / 10f + 3000f) * shades)) * ((int)shadeWidth);

						tilemap.SetTile(position, tileArray[index]);
					}
					else
					{
						if (Random.Range(0, shadeRarity) <= 0.2f) index = ((int)Mathf.Round(Mathf.PerlinNoise(x / 10f + 3000f, y / 10f + 3000f) * shades));
						else index = 0;

						tilemap.SetTile(position, defaultTileShades[index]);
					}
				}
			}
		}
	}
}