using SFML.Graphics;
using SFML.System;

class Tilemap
{
	public string LevelName;
	private string[] mapFile;
	private float tileSize;

	// All the stuff that gets rendered
	private RenderTexture backgroundFill;
	private RenderTexture background;
	private RenderTexture foreground;
	private RenderTexture lighting;



	// Create a new tilemap
	public Tilemap(string mapPath)
	{
		// Open the map file
		mapFile = File.ReadAllLines(mapPath);

		// Get map info
		tileSize = GetSingleValue<float>("tile-size");
		LevelName = GetSingleValue<string>("name");

		// Load the map
		LoadMap();
	}


	private void LoadMap()
	{
		// Generate the background fill
		{
			// Create a new render texture for drawing the background
			backgroundFill = new RenderTexture(Game.Window.Size.X, Game.Window.Size.Y);
			Texture backgroundTexture = new Texture(GetSingleValue<string>("background-fill"));

			// Get how many tiles will be drawn on the X and Y
			uint tilesX = (uint)Math.Ceiling(Game.Window.Size.X / tileSize);
			uint tilesY = (uint)Math.Ceiling(Game.Window.Size.Y / tileSize);

			// Loop through the entire window and create the background
			for (int y = 0; y < tilesY; y++)
			{
				for (int x = 0; x < tilesX; x++)
				{
					// Create the sprite
					Sprite backgroundTile = new Sprite(backgroundTexture);
					backgroundTile.Position = new Vector2f(x * tileSize, y * tileSize);

					// Add it to the background
					backgroundFill.Draw(backgroundTile);
				}
			}
			backgroundFill.Display();

		}
	
		// Generate the background layer (no collisions)
		{
			// Get the map and the map data
			string[] map = GetArrayValue("background-layer");
			int width = map[0].Length;
			int height = map.Length;

			// Create the render texture to make the map
			uint tilesX = (uint)(width * tileSize);
			uint tilesY = (uint)(height * tileSize);
			background = new RenderTexture(tilesX, tilesY);

			// Preload the textures
			// TODO: Don't hardcode this
			//! Hack solution
			Texture missing = new Texture("./assets/sprites/missing.png");
			Texture whiteWall = new Texture("./assets/sprites/white-wall.png");
			Texture blackWall = new Texture("./assets/sprites/black-wall.png");

			// Loop through the width and height and create the background
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					// Get the texture
					Texture texture = missing;
					if (map[y][x] == '0') texture = whiteWall;
					if (map[y][x] == '1') texture = blackWall;
					Sprite tile = new Sprite(texture);

					// Set the position
					tile.Position = new Vector2f(x * tileSize, y * tileSize);

					// Add it to the background
					background.Draw(tile);
				}
			}
			background.Display();
		}
	}


	// Render the map
	public void Render()
	{
		// Draw the background fill
		Game.Window.Draw(new Sprite(backgroundFill.Texture));
		Game.Window.Draw(new Sprite(background.Texture));
	}



	// Get a value from a map file
	private T GetSingleValue<T>(string key)
	{
		key = key.ToUpper() + ": ";
		foreach (string line in mapFile)
		{
			// Check for if the correct key is found
			if (!line.StartsWith(key)) continue;

			// Get the value of the key
			string keyValue = line.Replace(key, "").Trim();

			// Return the key as whatever type it is
			// TODO: Use switch
			if (typeof(T) == typeof(string)) return (T)(object)keyValue;
			else if (typeof(T) == typeof(uint)) return (T)(object)uint.Parse(keyValue);
			else if (typeof(T) == typeof(int)) return (T)(object)int.Parse(keyValue);
			else if (typeof(T) == typeof(float)) return (T)(object)float.Parse(keyValue);
			else if (typeof(T) == typeof(bool)) return (T)(object)bool.Parse(keyValue);

			// Stop the loop from going if the value has already been found
			break;
		}

		// Nothing was found
		return default(T);
	}

	private string[] GetArrayValue(string key)
	{
		List<string> array = new List<string>();
		bool foundKey = false;

		key = key.ToUpper();		
		foreach (string line in mapFile)
		{
			// Check for if the correct key is found
			if (line.StartsWith($"{key}[]:"))
			{
				foundKey = true;
				continue;
			}

			// Add the value
			if (foundKey)
			{
				// Check for if the array ended
				if (line.StartsWith(":") == false)
				{
					foundKey = false;
					break;
				}

				// Add the current line/value to the array
				array.Add(line.Replace(":", "").Trim());
			}
		}

		// Return the array
		return array.ToArray();
	}
}