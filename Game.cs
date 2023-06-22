using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }
	public static List<GameObject> GameObjects { get; set; }

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "2D Portal");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Clocks
		Clock deltaTimeClock = new Clock();



		while (Window.IsOpen)
		{
			// Handle events
			Window.DispatchEvents();
			DeltaTime = deltaTimeClock.Restart().AsSeconds();
			


            //TODO: Game logic and whatnot here




			// Clear the window
			Window.Clear(Color.Black);


			// TODO: Render/draw everything here


			// Display the contents of the window
			Window.Display();
		}
	}
}
