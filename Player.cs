using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Player : GameObject
{
	private Vector2f position;
	private Vector2f velocity;
	private Sprite sprite;
	private float moveForce = 1500f;
	private float mass = 25f;
	private float terminalVelocityX = 500f;
	private float friction;

	// New player constructor
	public Player(Vector2f spawnPoint)
	{
		this.position = spawnPoint;

		// Create the player sprite
		this.sprite = new Sprite(new Texture("./assets/sprites/player.png"));
		this.sprite.Scale = new Vector2f((Map.TileSize / sprite.Texture.Size.X), (Map.TileSize / sprite.Texture.Size.Y));
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


		// Apply gravity to the player
		velocity.Y += Map.Gravity * (Game.DeltaTime / mass);


		// Loop through all tiles in the current map
		for (int i = 0; i < Game.Map.Tiles.Length; i++)
		{
			// Get the current tile
			Tile tile = Game.Map.Tiles[i];
			if (tile.Settings.Solid == false) continue;

			// Check for if the player is on top/standing on the current tile
			{
				if (((newPosition.X + Map.TileSize) > tile.Position.X) && (newPosition.X < tile.Position.X))
				{
					if (((newPosition.Y + Map.TileSize) > tile.Position.Y) && (position.Y < tile.Position.Y))
					{
						// Place the player above the tile
						newPosition.Y = tile.Position.Y - Map.TileSize;

						// Reset downwards velocity
						velocity.Y = 0;

						// Set the friction to the current friction
						friction = tile.Settings.Friction;
					}
				}

			}


		}

		// Apply friction to slow down the player overtime
		velocity.X -= velocity.X * friction;
		if (Math.Abs(velocity.X) < 0.01f) velocity.X = 0f;

		// Console.WriteLine("Friction: " + friction);

		// Update the player position
		newPosition += velocity;
		position = newPosition;
	}
}