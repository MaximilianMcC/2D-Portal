using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Map
{
	public Tile[] Tiles { get; set; }
	public float TileSize { get; set; }

	// New map constructor
	public Map(float tileSize)
	{
		this.TileSize = tileSize;
	}

	// Load a map from a file
	public void LoadMap(string mapPath)
	{
		// Open the map file
		string[] mapFile = File.ReadAllLines(mapPath);
		List<Tile> mapTiles = new List<Tile>();
		List<(char, TileProperty, Sprite)> tileTypes = new List<(char, TileProperty, Sprite)>();
		int tileInfoBegin = 0;

		// Get all of the different tiles from the map file
		(char, TileProperty, Sprite) missingTileInfo = ('\0', new TileProperty(), new Sprite());
		{
			bool foundTileInfo = false;
			int tileIndex = 0;
			char currentTileCharacter = '\0';
			Sprite currentTileSprite = new Sprite();
			TileProperty currentTileProperty = new TileProperty();
			for (int i = 0; i < mapFile.Length; i++)
			{
				if (foundTileInfo)
				{
					// Check for what the current index is and assign the correct value
					string data = mapFile[i];
					
					// TODO: Use switch
					if (tileIndex == 0) currentTileCharacter = data[0];
					else if (tileIndex == 1) currentTileSprite = new Sprite(new Texture(data));
					else if (tileIndex == 2) currentTileProperty.Portalable = Boolean.Parse(data);
					else if (tileIndex == 3) currentTileProperty.Solid = Boolean.Parse(data);
					else if (tileIndex == 4) currentTileProperty.Friction = float.Parse(data);

					// Increase the current index for the next item
					tileIndex++;

					// Check for if another item needs to be added
					if (tileIndex >= 5)
					{
						// Create a new tile property, and reset everything
						Console.WriteLine("Resetting tiles");
						tileIndex = 0;
						tileTypes.Add((currentTileCharacter, currentTileProperty, currentTileSprite));
						currentTileCharacter = '\0';
						currentTileSprite = new Sprite();
						currentTileProperty = new TileProperty();
					}
				}
				
				// Check for if the current line is the tile info or not
				if (mapFile[i] == "TILE-INFO")
				{
					foundTileInfo = true;
					tileInfoBegin = i;
				}
			}
		}

		// Remove the tile info from the mapFile array so it just has the map data
		mapFile = mapFile.Take(tileInfoBegin).ToArray();

		// Loop through all characters in the map file
		for (int y = 0; y < mapFile.Length; y++)
		{
			for (int x = 0; x < mapFile[y].Length; x++)
			{
				// Get the tiles position
				Vector2f position = new Vector2f(x, y) * TileSize;

				// Get the tiles properties and sprite
				char tileCharacter = mapFile[y][x];
				(char, TileProperty, Sprite) tileInfo = missingTileInfo;
				for (int i = 0; i < tileTypes.Count; i++)
				{
					if (tileTypes[i].Item1 == tileCharacter) tileInfo = tileTypes[i];
				}

				// Create and add the tile to the map
				Tile tile = new Tile(position, tileInfo, TileSize);
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
	public FloatRect Bounds { get; private set; }
	private Sprite sprite;
	private bool highlighted = false;

	public Tile(Vector2f position, (char, TileProperty, Sprite) tileInfo, float tileSize)
	{
		this.Position = position;
		this.Properties = tileInfo.Item2;

		sprite = new Sprite(tileInfo.Item3);
		sprite.Position = position;
		Bounds = sprite.GetGlobalBounds();
	}

	// Highlight a tile. Debug only
	public void ToggleHighlight()
	{
		if (highlighted) sprite.Color = Color.Red;
		else sprite.Color = Color.Transparent;
	}

	// Draw the tile to the screen
	public void Render()
	{
		Game.Window.Draw(sprite);
	}
}