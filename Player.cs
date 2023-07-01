using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	private PortalGun portalGun;
	private Vector2f velocity;
	private float moveForce = 1500f;
	private float mass = 70f; //* 70kg 

	// TODO: Make these part of a map or something
	private float friction = 0.1f;
	private float gravity = 9.81f;


	// Create a new player
	public Player(Vector2f spawnPoint)
	{
		// Create the player sprite
		sprite = new Sprite(new Texture("./assets/sprites/player.png"));

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
	}





	// Move the player
	private void Movement()
	{
		Vector2f newPosition = Position;

		// Get player movement input
		if (InputManager.KeyHeld(InputManager.Inputs.MoveLeft)) velocity.X -= moveForce * (Game.DeltaTime / mass);
		if (InputManager.KeyHeld(InputManager.Inputs.MoveRight)) velocity.X += moveForce * (Game.DeltaTime / mass);
		
		// Apply friction to slow down the player over time
		velocity.X -= velocity.X * friction;
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		// Update the position
		newPosition += velocity;
		Position = newPosition;
	}

	// Check for if the player want to shoot a portal
	private void Shoot()
	{
		if (InputManager.MouseClicked(InputManager.Inputs.FireBlue)) portalGun.ShootPortal(PortalType.BLUE);
		if (InputManager.MouseClicked(InputManager.Inputs.FireOrange)) portalGun.ShootPortal(PortalType.ORANGE);
	}

}