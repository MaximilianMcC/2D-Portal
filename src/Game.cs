using System.Data;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public const float Gravity = 9.81f;
	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }
	public static Discord Discord { get; private set; }
	public static List<GameObject> GameObjects;
	public static Tilemap Map { get; set; }
	private bool debugMode = false;
	private Player player;

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(1422, 800), "2D Portal");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();
		Window.SetIcon(32, 32, new Image("./assets/icons/icon-text.png").Pixels);
		Listener.GlobalVolume = 5;

		// Clocks
		Clock deltaTimeClock = new Clock();

		// Store all game objects
		// TODO: Instantiate game objects in another class or something to keep clean
		GameObjects = new List<GameObject>();
		player = new Player(new Vector2f(100, 200));

		// Create test portals
		BluePortal bluePortal = new BluePortal(new Vector2f(10, 200), Direction.LEFT);
		OrangePortal orangePortal = new OrangePortal(new Vector2f(300, 400), Direction.DOWN);
		bluePortal.OrangePortal = orangePortal;
		orangePortal.BluePortal = bluePortal;

		// Load the map
		Map = new Tilemap("./assets/maps/debug.txt");

		// Run all of the game objects start methods
		for (int i = 0; i < GameObjects.Count; i++) GameObjects[i].Start();

		// Start Discord rich presence
		// TODO: Don't make public to game class
		Discord = new Discord();
		Discord.Start();
		Discord.UpdateState(State.PLAYING);
		Game.Discord.UpdateDetails(Map.LevelName);

		while (Window.IsOpen)
		{
			// Handle events
			Window.DispatchEvents();
			DeltaTime = deltaTimeClock.Restart().AsSeconds();


			// Update everything
			Update();


			// Clear the screen, then render everything
			Window.Clear(Color.Blue);
			Render();
			Window.Display();
		}
	}


	// Do all the game logic and whatnot
	private void Update()
	{
		// Update all of the game objects
		for (int i = 0; i < GameObjects.Count; i++) GameObjects[i].Update();

		// Check for if they want to enable/disable debug console
		// if (InputManager.KeyPressed(InputManager.Inputs.ToggleDebugMode)) debugMode = !debugMode;
		if (InputManager.KeyPressed(InputManager.Inputs.ToggleDebugMode))
		{
			debugMode = !debugMode;
			if (debugMode) Discord.UpdateState(State.DEBUGGING);
			else Discord.UpdateState(State.PLAYING);
		}
	}

	// Draw everything to the screen
	private void Render()
	{
		// Show fps
		// TODO: Put this in debug class
		Debug.LogValue("FPS", (int)(1f / DeltaTime));

		// Draw the map
		Map.Render();

		// Render all of the game objects
		for (int i = 0; i < GameObjects.Count; i++) GameObjects[i].Render();

		// Show the debug console if debug mode is enabled
		if (debugMode) Debug.RenderDebugMessages();
	}
}
