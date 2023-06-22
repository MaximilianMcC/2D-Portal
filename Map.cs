using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Map
{
	public static float TileSize;
	private List<Tile> map = new List<Tile>();
	private List<TileSettings> tiles = new List<TileSettings>();

	public Map(string mapName)
	{
		// Create/add all of the tiles
		// TODO: Do this in another place
		TileSettings missing = new TileSettings(' ', false, "missing");
		tiles.Add(new TileSettings('b', false, "black-wall")); // Black wall cubes
		tiles.Add(new TileSettings('w', false, "white-wall")); // White wall tiles
		

		// Load a new map from a map file
		string[] mapFile = File.ReadAllLines($"./assets/maps/{mapName}.map");
		TileSize = Game.Window.Size.X / mapFile.Length;


		// Generate the map
		for (int i = 0; i < mapFile.Length; i++)
		{
			// For each tile in the map
			for (int j = 0; j < mapFile[i].Length; j++)
			{
				// Create/get the tile settings
				TileSettings settings = missing;
				for (int k = 0; k < tiles.Count; k++)
				{
					if (mapFile[i][j] == tiles[k].Character) settings = tiles[k];
				}

				// Make the new tile, and add it to the map
				Tile tile = new Tile(settings, new Vector2f((j * TileSize), (i * TileSize)));
				map.Add(tile);
			}
		}
	}

	// Draw the map
	public void DrawMap()
	{
		for (int i = 0; i < map.Count; i++)
		{
			Game.Window.Draw(map[i].Sprite);
		}
	}
}



// Tile properties and whatnot
struct TileSettings
{
	public char Character { get; private set; }
	public bool Solid { get; private set; }
	public string TextureName { get; private set; }

	// New tile constructor
	public TileSettings(char mapFileCharacter, bool solid, string textureName)
	{
		this.Character = mapFileCharacter;
		this.Solid = solid;
		this.TextureName = textureName;
	}
}

class Tile
{
	public Vector2f Position { get; set; }
	public TileSettings Settings { get; private set; }
	public Sprite Sprite { get; private set; }

	public Tile(TileSettings settings, Vector2f position)
	{
		this.Position = position;
		this.Settings = settings;

		// Create the sprite
		this.Sprite = new Sprite(new Texture($"./assets/sprites/{settings.TextureName}.png"));
		Sprite.Scale = new Vector2f((Map.TileSize / Sprite.Texture.Size.X), (Map.TileSize / Sprite.Texture.Size.Y));
		Sprite.Position = Position;
	}
}