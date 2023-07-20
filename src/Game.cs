using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public const float Gravity = 9.81f;
	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }
	public static List<GameObject> GameObjects;
	private bool debugMode = false;

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
		Player player = new Player();


		// Run all of the game objects start methods
		for (int i = 0; i < GameObjects.Count; i++) GameObjects[i].Start();


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
			Window.Display();
		}
	}


	// Do all the game logic and whatnot
	private void Update()
	{;
		// Update all of the game objects
		for (int i = 0; i < GameObjects.Count; i++) GameObjects[i].Update();

		// Check for if they want to enable/disable debug console
		if (InputManager.KeyPressed(InputManager.Inputs.ToggleDebugMode)) debugMode = !debugMode;
	}

	// Draw everything to the screen
	private void Render()
	{
		// Render all of the game objects
		for (int i = 0; i < GameObjects.Count; i++) GameObjects[i].Render();

		// Show the debug console if debug mode is enabled
		if (debugMode) Debug.RenderDebugMessages();
	}
}
