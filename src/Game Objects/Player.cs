using SFML.Graphics;
using SFML.System;

class Player : GameObject
{
	public Direction direction;
	private float moveForce = 2700f;
	private float mass = 60f;
	private float frictionCoefficient = 0.2f;
	private Vector2f velocity;
	private float width;


	public Player(Vector2f spawnPoint)
	{
		// Set the players spawn point
		Position = spawnPoint;

		// Create the sprite
		Sprite = new Sprite(new Texture("./assets/sprites/player/player-5.png"));
		width = Sprite.GetLocalBounds().Width;
	}


	public override void Update()
	{
		base.Update();


		// TODO: Split up player movement into another class
		Move();

		Debug.LogValue("Player Position", Position);
		Debug.LogValue("Player Velocity", velocity);
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
		if (Math.Abs(velocity.X) < 0.1f) velocity.X = 0f;

		// Apply gravity
		velocity.Y += Game.Gravity * Game.DeltaTime;

		// Update the players position according to velocity
		newPosition += velocity;

		// Check for collision on the X, and Y axis
		newPosition.Y = CollisionY(newPosition);
		newPosition.X = CollisionX(newPosition);

		// Actually move the players
		Position = newPosition;


		// Update the players movement direction
		direction = velocity.X >= 0 ? Direction.RIGHT : Direction.LEFT;
	}

	// Check for X collisions
	private float CollisionX(Vector2f newPosition)
	{
		// Create the new collision
		FloatRect playerCollision = new FloatRect(newPosition, new Vector2f(Bounds.Width, Bounds.Height));

		foreach (FloatRect tile in Game.Map.Collisions)
		{
			// Check for if the player collides with the current tile
			if (playerCollision.Intersects(tile))
			{
				// Stop the player from moving, and give back the current position
				velocity.X = 0;
				return Position.X;
			}
		}

		return newPosition.X;
	}


	// Check for Y collisions
	private float CollisionY(Vector2f newPosition)
	{
		// Create the new collision
		FloatRect playerCollision = new FloatRect(newPosition, new Vector2f(Bounds.Width, Bounds.Height));

		foreach (FloatRect tile in Game.Map.Collisions)
		{
			// Check for if the player collides with the current tile
			if (playerCollision.Intersects(tile))
			{
				// Stop the player from moving, and give back the current position
				velocity.Y = 0;
				Console.WriteLine("collision!!!!!!!!!!!!!!");
				return Position.Y;
			}

		}

		return newPosition.Y;
	}
}