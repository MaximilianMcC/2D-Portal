using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }
	public static List<GameObject> GameObjects { get; set; }
	public static readonly float Gravity = 9.81f;

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "2D Portal");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Create a new list to store all of the game objects
		GameObjects = new List<GameObject>();


		// Clocks
		Clock deltaTimeClock = new Clock();

		// Create the map
		Map map = new Map("level-0");

		// Create the player
		Player player = new Player(new Vector2f(200, 200));

	
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

			// Update all game objects
			for (int i = 0; i < GameObjects.Count; i++)
			{
				GameObjects[i].Update();
			}



			// Clear the window
			Window.Clear(Color.Magenta);

			// Draw the map
			map.DrawMap();

			// Draw all game objects
			for (int i = 0; i < GameObjects.Count; i++)
			{
				GameObjects[i].Render();
			}


			// Display the contents of the window
			Window.Display();
		}
	}
}
