using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	private PortalGun portalGun;
	private Vector2f velocity;
	private float moveForce = 1500f;
	private float mass = 70f; //* 70kg 
	private float gravity = 9.81f;
	private FloatRect bounds;

	// Create a new player
	public Player(Vector2f spawnPoint)
	{
		// Create the player sprite
		sprite = new Sprite(new Texture("./assets/sprites/player/player-1.png"));
		bounds = sprite.GetGlobalBounds();

		// Assign the position from the spawnpoint
		Position = spawnPoint;



		// Create the portal gun
		portalGun = new PortalGun();
	}

	// Update the player
	public override void Update()
	{
		bounds = sprite.GetGlobalBounds();
		Debug.LogValue("Player bounds: ", bounds);

		Movement();
		Shoot();
	}





	// Move the player
	private void Movement()
	{
		Vector2f newPosition = Position;

		// Get player movement input
		if (InputManager.KeyHeld(InputManager.Inputs.MoveLeft)) velocity.X -= moveForce * (Game.DeltaTime / mass);
		if (InputManager.KeyHeld(InputManager.Inputs.MoveRight)) velocity.X += moveForce * (Game.DeltaTime / mass);
		
		// Apply friction to slow down the player over time
		ApplyFriction(newPosition);

		// Update the position
		newPosition += velocity;
		Position = newPosition;
	}
		
	// Apply friction to slow down the player over time
	private void ApplyFriction(Vector2f newPosition)
	{
		float friction = TileWherePlayerIs(newPosition).Properties.Friction;
		velocity.X -= velocity.X * friction;
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		Debug.LogValue("Friction", friction);
	}



	// Check for if the player want to shoot a portal
	private void Shoot()
	{
		if (InputManager.MouseClicked(InputManager.Inputs.FireBlue)) portalGun.ShootPortal(PortalType.BLUE);
		if (InputManager.MouseClicked(InputManager.Inputs.FireOrange)) portalGun.ShootPortal(PortalType.ORANGE);
	}










	// Get the tile that the player is currently standing on
	private Tile TileWherePlayerIs(Vector2f playerPosition)
	{
		foreach (Tile tile in Game.Map.Tiles)
		{
			if (bounds.Intersects(tile.Bounds)) return tile;
		}

		return null;
	}
}