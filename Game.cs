using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }
	public static List<GameObject> GameObjects { get; set; }
	public static Map Map { get; private set; }

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "2D Portal");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();
		Window.SetIcon(32, 32, new Image("./assets/icons/icon-text.png").Pixels);
		bool debugMode = true;

		// Create a new list to store all of the game objects
		GameObjects = new List<GameObject>();


		// Clocks
		Clock deltaTimeClock = new Clock();

		// Create the map
		Map = new Map("level-0");

		// Create the player and a few cubes
		Player player = new Player(new Vector2f(0, (11 * Map.TileSize)));
		WeightedCube weightedCube = new WeightedCube(new Vector2f(500, (11 * Map.TileSize)));
		CompanionCube companionCube = new CompanionCube(new Vector2f(600, (11 * Map.TileSize)));
	
		// Run all start methods
		for (int i = 0; i < GameObjects.Count; i++)
		{
			GameObjects[i].Start();
		}


		while (Window.IsOpen)
		{
			// Handle events
			Window.DispatchEvents();
			DeltaTime = deltaTimeClock.Restart().AsSeconds();
			Debug.Log("2D-Portal version 1.0a");

			// Update all game objects
			for (int i = 0; i < GameObjects.Count; i++)
			{
				GameObjects[i].Update();
			}



			// Clear the window
			Window.Clear(Color.Magenta);

			// Draw the map
			Map.DrawMap();

			// Draw all game objects
			for (int i = 0; i < GameObjects.Count; i++)
			{
				GameObjects[i].Render();
			}

			// Display the debug info
			if (debugMode) Debug.RenderDebugMessages();

			// Display the contents of the window
			Window.Display();
		}
	}
}
