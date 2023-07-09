using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	private PortalGun portalGun;
	private Vector2f velocity;
	private float moveForce = 1500f;
	private float mass = 70f; //* 70kg 
	private float friction = 0.01f;
	private Direction direction;

	// Create a new player
	public Player(Vector2f spawnPoint)
	{
		// Create the player sprite
		sprite = new Sprite(new Texture("./assets/sprites/player/player-1.png"));
		direction = Direction.RIGHT;
		// TODO: Don't change the origin
		sprite.Origin = new Vector2f((sprite.Texture.Size.X / 2), 0);


		// Assign the position from the spawnpoint
		Position = spawnPoint;



		// Create the portal gun
		portalGun = new PortalGun();
	}

	// Update the player
	public override void Update()
	{
		Movement();
		Shoot();
		Animate();
	}





	// Move the player
	private void Movement()
	{
		Vector2f newPosition = Position;

		// Get player movement input
		if (InputManager.KeyHeld(InputManager.Inputs.MoveLeft))
		{
			velocity.X -= moveForce * (Game.DeltaTime / mass);
			direction = Direction.LEFT;
		}
		if (InputManager.KeyHeld(InputManager.Inputs.MoveRight))
		{
			velocity.X += moveForce * (Game.DeltaTime / mass);
			direction = Direction.RIGHT;
		}
		
		// Apply friction to slow down the player over time
		velocity.X -= velocity.X * friction;
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		// Apply gravity to move the player downwards
		velocity.Y += Game.Gravity;

		// Update the position
		newPosition += velocity;

		// Check for collision
		foreach (Tile tile in Game.Map.Tiles)
		{
			// Get all solid tiles that the player is colliding with
			if (tile.Properties.Solid == true && CollidingWithTile(newPosition, tile))
			{
				// Stop the player and disregard the new movement
				velocity.X = 0f;
				velocity.Y = 0f;

				// Adjust the position of the player to allow them to move
				newPosition.Y = tile.Position.Y - Game.Map.TileSize;
				break;
			}
		}

		// Actually move the player
		Position = newPosition;


		//! debug
		Debug.LogValue("Player position: ", Position);
		Debug.LogValue("Player velocity: ", velocity);
	}
		


	// Check for if the player want to shoot a portal
	private void Shoot()
	{
		// if (InputManager.MouseClicked(InputManager.Inputs.FireBlue)) portalGun.ShootPortal(PortalType.BLUE);
		// if (InputManager.MouseClicked(InputManager.Inputs.FireOrange)) portalGun.ShootPortal(PortalType.ORANGE);
	}










	// Check for if the player is colliding with a tile
	private bool CollidingWithTile(Vector2f newPosition, Tile tile)
	{
		// Calculate the player bounds based on the new position
		FloatRect bounds = new FloatRect(newPosition, new Vector2f(Game.Map.TileSize, Game.Map.TileSize));

		// Check for collision
		return bounds.Intersects(tile.Bounds);
	}








	// Animate the player
	private void Animate()
	{
		// Check for what direction the player is, then flip them
		sprite.Scale = new Vector2f((int)direction, sprite.Scale.Y);

		// TODO: Animate the player
	}
}





enum Direction
{
	LEFT = -1,
	RIGHT = 1
}