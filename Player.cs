using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{

	// Visual based values
	private Sprite sprite;

	// Movement based values
	private Vector2f velocity;
	private float moveForce = 1500f;
	private float mass = 25f;
	private float terminalVelocityX = 20f;
	private float friction = 0.15f;

	// Inventory based values
	private float itemPickUpProximity = 100f;
	private Vector2f heldItemOffset = new Vector2f(65f, -15f);
	private bool pressedAKey;

	// New player constructor
	public Player(Vector2f spawnPoint)
	{
		this.Position = spawnPoint;

		// Create the player sprite
		this.sprite = new Sprite(new Texture("./assets/sprites/player.png"));
		this.sprite.Scale = new Vector2f((Map.TileSize / sprite.Texture.Size.X), (Map.TileSize / sprite.Texture.Size.Y));
		sprite.Position = Position;

		// Add the player to the list of game objects
		Game.GameObjects.Add(this);
	}

	// Runs before the update script
	public override void Start()
	{
		Console.WriteLine("player");
	}

	// Add logic every frame
	public override void Update()
	{
		Movement();
		ItemPickUp();
	}
	
	// Get the bounds of the player
	public FloatRect GetBounds()
	{
		return sprite.GetGlobalBounds();
	}



	// Make the player move and interact with the map
	private void Movement()
	{
		// Calculate movement stuff
		Vector2f newPosition = Position;

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

		// Apply friction to slow down the player overtime
		velocity.X -= velocity.X * friction;
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		// Update the player position
		newPosition += velocity;
		Position = newPosition;


		Debug.LogValue("X Velocity: ", velocity.X);
		Debug.LogValue("X Position: ", Position.X);
	}

	// Make the player pickup items that are within a certain proximity
	private void ItemPickUp()
	{
		// Make a circle to act as the pickup area
		// TODO: If using a circle is bad for performance, do some fancy maths
		CircleShape circle = new CircleShape(itemPickUpProximity);
		circle.Origin = new Vector2f(itemPickUpProximity, itemPickUpProximity);
		circle.Position = new Vector2f(Position.X + (Map.TileSize / 2), Position.Y + (Map.TileSize / 2));

		// Check for if there is an item inside of the circle
		for (int i = 0; i < Game.GameObjects.Count; i++)
		{
			// Get the thing that was in the circle
			if (Game.GameObjects[i] == this) continue;
			GameObject gameObject = Game.GameObjects[i];

			if (circle.GetGlobalBounds().Intersects(gameObject.GetBounds()))
			{
				// Check for if the player is pressing the pickup button
				if (Keyboard.IsKeyPressed(InputManager.InteractKey) && pressedAKey == false)
				{
					pressedAKey = true;
				}

				Debug.LogValue("GameObject collision: ", true);
			}
		}
	}
}