using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	private Vector2f position;
	private Vector2f velocity;
	private Sprite sprite;
	private float moveForce = 300f;
	private float mass = 25f;
	private float terminalVelocityX = 100f;
	private float friction = 0.1f; // TODO: Put this in the tile the player is standing on

	// New player constructor
	public Player(Vector2f spawnPoint)
	{
		// Create the player sprite
		this.sprite = new Sprite(new Texture("./assets/sprites/missing.png"));
		this.position = spawnPoint;
		sprite.Position = position;

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
		Console.WriteLine("Player Velocity: " + velocity);
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
			// Apply a force to move the player left
			velocity.X -= moveForce * (Game.DeltaTime / mass);
		}
		if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
		{
			velocity.X += moveForce * (Game.DeltaTime / mass);
		}

		// Clamp the velocity to stop the player going out of control
		velocity.X = Math.Clamp(velocity.X, -terminalVelocityX, terminalVelocityX);

		// Apply friction to slow down the player overtime
		velocity.X -= velocity.X * friction;
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		// Update the player position
		newPosition.X += velocity.X;
		position = newPosition;
	}
}