using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	private Vector2f velocity;
	private float moveForce = 1500f;
	private float mass = 70f; //* 70kg 
	private float friction = 0.1f;


	// Create a new player
	public Player(Vector2f spawnPoint)
	{
		// Create the player sprite
		sprite = new Sprite(new Texture("./assets/sprites/player.png"));

		// Assign the position from the spawnpoint
		Position = spawnPoint;
	}

	// Update the player
	public override void Update()
	{
		Movement();

	}






	private void Movement()
	{
		Vector2f newPosition = Position;

		// Get player movement input
		if (InputManager.KeyHeld(InputManager.Keys.MoveLeft)) velocity.X -= moveForce * (Game.DeltaTime / mass);
		if (InputManager.KeyHeld(InputManager.Keys.MoveRight)) velocity.X += moveForce * (Game.DeltaTime / mass);
		
		// Apply friction to slow down the player over time
		velocity.X -= velocity.X * friction;
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		// Update the position
		newPosition += velocity;
		Position = newPosition;

		// Debug values
		// TODO: Remove these
		Debug.LogValue("Player X Velocity: ", velocity.X);
		Debug.LogValue("Player X Position: ", Position.X);
	}

	
}