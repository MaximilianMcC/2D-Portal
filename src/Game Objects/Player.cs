using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	public float moveForce { get; set; } = 2000f;
	private float mass = 60f;
	private float frictionCoefficient = 0.1f;
	private Vector2f velocity;


	public Player()
	{
		// Create the sprite
		Sprite = new Sprite(new Texture("./assets/sprites/player/player-5.png"));
	}


	public override void Update()
	{
		// TODO: Split up player movement into another class
		Move();
	}

	// Move the player
	private void Move()
	{
		// Calculate the new movement
		Vector2f newPosition = Position;
		float acceleration = moveForce / mass;
		float movement = acceleration * Game.DeltaTime;

		// Get player movement input, then apply force to move the player
		if (InputManager.KeyHeld(InputManager.Inputs.MoveLeft)) velocity.X -= movement;
		if (InputManager.KeyHeld(InputManager.Inputs.MoveRight)) velocity.X += movement;

		// Apply friction on the X to slow down the player overtime
		velocity.X -= (frictionCoefficient * velocity.X);
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		// Update the players position
		newPosition += velocity;
		Position = newPosition;
	}
}