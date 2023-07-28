using SFML.Graphics;
using SFML.System;

class Tilemap
{
	public string LevelName;
	private string[] mapFile;
	private float tileSize;

	// All the stuff that gets rendered
	private RenderTexture background;



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
			background = new RenderTexture(Game.Window.Size.X, Game.Window.Size.Y);
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
					background.Draw(backgroundTile);
				}
			}
			background.Display();

		}
	
		// Generate the background layer (no collisions)
		{
			string[] map = GetArrayValue("background-layer");
			foreach (var item in map)
			{
				Console.WriteLine(item);
			}
		}
	}


	// Render the map
	public void Render()
	{
		// Draw the background fill
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
				// Add the current line/value to the array
				array.Add(line.Replace(";", "").Trim());

				// Check for if the array ended
				if (line.EndsWith(";"))
				{
					foundKey = false;
					break;
				}
			}
		}

		// Return the array
		return array.ToArray();
	}
}