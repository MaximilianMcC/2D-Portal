using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Map
{
	//! Try not to use static stuff in here. If there are multiple maps the whole thing can break I think but idk
	public static float TileSize { get; private set; }
	public static float Gravity = 9.81f;
	public Tile[] Tiles { get; private set; }
	private List<TileSettings> tileSettings = new List<TileSettings>();

	public Map(string mapName)
	{
		// Create/add all of the tiles
		// TODO: Do this in another place
		TileSettings missing = new TileSettings(' ', false, 0.01f, "missing");
		tileSettings.Add(new TileSettings('b', true, 0.1f, "black-wall")); // Black wall cubes
		tileSettings.Add(new TileSettings('w', false, 0.01f, "white-wall")); // White wall tiles
		tileSettings.Add(new TileSettings('t', true, 0.01f, "missing")); // Test
		

		// Load a new map from a map file
		string[] mapFile = File.ReadAllLines($"./assets/maps/{mapName}.map");
		TileSize = Game.Window.Size.X / mapFile.Length;


		// Generate the map
		List<Tile> map = new List<Tile>();
		for (int i = 0; i < mapFile.Length; i++)
		{
			// For each tile in the map
			for (int j = 0; j < mapFile[i].Length; j++)
			{
				// Create/get the tile settings
				TileSettings settings = missing;
				for (int k = 0; k < tileSettings.Count; k++)
				{
					if (mapFile[i][j] == tileSettings[k].Character) settings = tileSettings[k];
				}

				// Make the new tile, and add it to the map
				Tile tile = new Tile(settings, new Vector2f((j * TileSize), (i * TileSize)));
				map.Add(tile);
			}
		}

		// Convert the map list into a tiles array to make it faster
		Tiles = map.ToArray();
	}

	// Draw the map
	public void DrawMap()
	{
		for (int i = 0; i < Tiles.Length; i++)
		{
			Game.Window.Draw(Tiles[i].Sprite);
		}
	}
}



// Tile properties and whatnot
struct TileSettings
{
	// Properties
	public bool Solid { get; private set; }
	public float Friction { get; private set; }

	// Stuff used for generating the tile
	// TODO: Find a way to remove these
	public char Character { get; private set; }
	public string TextureName { get; private set; }

	// New tile constructor
	public TileSettings(char mapFileCharacter, bool solid, float friction, string textureName)
	{
		this.Solid = solid;
		this.Friction = friction;

		this.Character = mapFileCharacter;
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