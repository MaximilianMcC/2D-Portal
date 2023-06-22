using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	private Vector2f position;
	private float height;
	private Vector2f velocity;
	private Sprite sprite;
	private float moveForce = 300f;
	private float mass = 25f;
	private float terminalVelocityX = 100f;

	// New player constructor
	public Player(Vector2f spawnPoint)
	{
		// Create the player sprite
		this.sprite = new Sprite(new Texture("./assets/sprites/missing.png"));
		this.position = spawnPoint;
		sprite.Position = position;
		this.height = Map.TileSize;

		// Add the player to the list of game objects
		Game.GameObjects.Add(this);
	}

	public void Start()
	{
		Console.WriteLine("player");
	}

	public void Update()
	{
		Movement();
	}

	// Render the player
	public void Render()
	{
		sprite.Position = position;
		Game.Window.Draw(sprite);
	}






	private void Movement()
	{
		// Calculate movement stuff
		Vector2f newPosition = position;

		// Get player movement input
		if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
		{
			// Apply a force to move the player to the left
			velocity.X -= moveForce * (Game.DeltaTime / mass);
		}
		if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
		{
			// Apply a force to move the player to the right
			velocity.X += moveForce * (Game.DeltaTime / mass);
		}

		// Clamp the velocity to stop the player going out of control
		velocity.X = Math.Clamp(velocity.X, -terminalVelocityX, terminalVelocityX);

		// Loop through all tiles in the current map
		for (int i = 0; i < Game.Map.Tiles.Length; i++)
		{
			// Get the current tile
			Tile tile = Game.Map.Tiles[i];
			if (tile.Settings.Solid != true) continue;

			// Check for if the player is on top/standing on the current tile
			if (position == tile.Position) // TODO: actual do it
			{
				
				// Apply friction to slow down the player overtime
				velocity.X -= velocity.X * tile.Settings.Friction;
				if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;
			}

		}


		// Update the player position
		newPosition.X += velocity.X;
		position = newPosition;
	}
}