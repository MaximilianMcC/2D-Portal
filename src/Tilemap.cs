using SFML.Graphics;
using SFML.System;

class Tilemap
{
	public string LevelName;
	private string[] mapFile;
	private int tileSize;

	// Create a new tilemap
	public Tilemap(string mapPath)
	{
		// Open the map file
		mapFile = File.ReadAllLines(mapPath);

		// Get map info
		tileSize = GetValue<int>("tile-size");
		LevelName = GetValue<string>("name");
	}



	// Render the map
	public void Render()
	{
		// Draw the background fill
	}




	// Get a value from a map file
	private T GetValue<T>(string key)
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
			else if (typeof(T) == typeof(int)) return (T)(object)int.Parse(keyValue);
			else if (typeof(T) == typeof(float)) return (T)(object)float.Parse(keyValue);
			else if (typeof(T) == typeof(bool)) return (T)(object)bool.Parse(keyValue);

			// Stop the loop from going if the value has already been found
			break;
		}

		// Nothing was found
		return default(T);
	}
}