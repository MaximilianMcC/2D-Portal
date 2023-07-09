using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }
	public static List<GameObject> GameObjects { get; set; }
	public static Map Map { get; private set; }
	public static Player Player { get; private set; }

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(1422, 800), "2D Portal");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();
		Window.SetIcon(32, 32, new Image("./assets/icons/icon-text.png").Pixels);

		// Create a new list to store all of the game objects
		GameObjects = new List<GameObject>();
		bool debugMode = true;


		// Clocks
		Clock deltaTimeClock = new Clock();

		// Create the map
		Map = new Map(32); // 32px idk
		Map.LoadMap("./assets/maps/level-0.map");

		// Create the player and a few cubes
		Player = new Player(new Vector2f(1000, 100));
	
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


			// Update everything
			Update();


			// Clear the screen, then render everything
			Window.Clear(Color.Magenta);
			Render();
			if (debugMode) Debug.RenderDebugMessages();
			Window.Display();
		}
	}



	private void Update()
	{
		// Update all game objects
		for (int i = 0; i < GameObjects.Count; i++)
		{
			GameObjects[i].Update();
		}
	}

	private void Render()
	{
		// Draw the map
		Map.RenderMap();

		// Draw all game objects
		for (int i = 0; i < GameObjects.Count; i++)
		{
			GameObjects[i].Render();
		}
	}
}
