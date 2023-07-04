using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Map
{
	public Tile[] Tiles { get; set; }
	private float tileSize;

	// New map constructor
	public Map(float tileSize)
	{
		this.tileSize = tileSize;
	}

	// Load a map from a file
	public void LoadMap(string mapPath)
	{
		// Open the map file
		string[] mapFile = File.ReadAllLines(mapPath);
		List<Tile> mapTiles = new List<Tile>();
		List<(char, TileProperty, Sprite)> tileTypes = new List<(char, TileProperty, Sprite)>();

		// Get all of the different tiles from the map file
		bool foundTileInfo = false;
		int tileIndex = 0;
		char currentTileCharacter = '\0';
		Sprite currentTileSprite = new Sprite();
		TileProperty currentTileProperty = new TileProperty();
		(char, TileProperty, Sprite) missingTileInfo = ('\0', new TileProperty(), new Sprite());
		for (int i = 0; i < mapFile.Length; i++)
		{
			// Check for if the current line is the tile info or not
			if (mapFile[i] == "TILE-INFO")
			{
				foundTileInfo = true;
				continue;
			}
			if (!foundTileInfo) continue;
			
			// Check for what the current index is and assign the correct value
			// TODO: Use switch
			if (tileIndex == 0) currentTileCharacter = mapFile[i][0];
			else if (tileIndex == 1) currentTileSprite = new Sprite(new Texture(mapFile[i]));
			else if (tileIndex == 2) currentTileProperty.Portalable = Boolean.Parse(mapFile[i]);
			else if (tileIndex == 3) currentTileProperty.Solid = Boolean.Parse(mapFile[i]);
			else if (tileIndex == 4) currentTileProperty.Friction = float.Parse(mapFile[i]);
			else
			{
				// Create a new tile property, and reset everything
				tileIndex = 0;
				tileTypes.Add((currentTileCharacter, currentTileProperty, currentTileSprite));
				currentTileCharacter = '\0';
				currentTileSprite = new Sprite();
				currentTileProperty = new TileProperty();
			}

			// Increase the current index for the next item
			tileIndex++;
		}

		// Loop through all characters in the map file
		for (int y = 0; y < mapFile.Length; y++)
		{
			for (int x = 0; x < mapFile[y].Length; x++)
			{
				// Get the tiles position
				Vector2f position = new Vector2f(x, y) * tileSize;

				// Get the tiles properties and sprite
				char tileCharacter = mapFile[y][x];
				(char, TileProperty, Sprite) tileInfo = missingTileInfo;
				for (int i = 0; i < tileTypes.Count; i++)
				{
					if (tileTypes[i].Item1 == tileCharacter) tileInfo = tileTypes[i];
				}

				// Create and add the tile to the map
				Tile tile = new Tile(position, tileInfo);
				mapTiles.Add(tile);
			}
		}

		// Convert the map list to an array because its slightly faster
		Tiles = mapTiles.ToArray();
	}

	// Render the mao
	public void RenderMap()
	{
		for (int i = 0; i < Tiles.Length; i++)
		{
			Tiles[i].Render();
		}
	}
}




struct TileProperty
{
	public bool Portalable { get; set; }
	public bool Solid { get; set; }
	public float Friction { get; set; }
}


class Tile
{
	public Vector2f Position { get; set; }
	public TileProperty Properties { get; set; }
	Sprite sprite;

	public Tile(Vector2f position, (char, TileProperty, Sprite) tileInfo)
	{
		this.Position = position;
		this.Properties = tileInfo.Item2;
		sprite = new Sprite(tileInfo.Item3);
	}

	// Draw the tile to the screen
	public void Render()
	{
		Game.Window.Draw(sprite);
	}
}