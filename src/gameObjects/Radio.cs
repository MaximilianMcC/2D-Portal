using SFML.Audio;
using SFML.Graphics;
using SFML.System;

class Radio : GameObject
{
	Sound radio;
	float minDistance = 300f; // The distance where the radio sound is at 100% volume
	float maxDistance = 1000f; // The distance where the radio sound fades out to 0% volume
	private Vector2f velocity;

	// Create a new radio
	public Radio(Vector2f spawnPoint)
	{
		this.Position = spawnPoint;

		// Create the sprite
		this.sprite = new Sprite(new Texture("./assets/sprites/radio.png"));

		// Create the radio
		string musicPath = "./assets/sounds/radio.wav";
		radio = new Sound(new SoundBuffer(musicPath));
		radio.Loop = true;
	}

	// When the game starts
	public override void Start()
	{
		// Start to play the radio
		radio.Play();
	}

	// Update
	public override void Update()
	{
		ChangeVolume();
		ApplyGravity();
	}

	// Add gravity to the radio
	public void ApplyGravity()
	{
		Vector2f newPosition = Position;

		// Apply gravity to move the radio downwards
		velocity.Y += Game.Gravity;

		// Update the position
		newPosition += velocity;

		// Check for collision
		foreach (Tile tile in Game.Map.Tiles)
		{
			// Get all solid tiles that the player is colliding with
			if (tile.Properties.Solid == true && CollidingWithTile(newPosition, tile))
			{
				// Stop the player from moving downwards
				velocity.Y = 0f;

				// Adjust the position of the player to allow them to move
				newPosition.Y = tile.Position.Y - Game.Map.TileSize;
				break;
			}
		}

		// Update the position
		Position = newPosition;

		Debug.LogValue("Radio y: ", velocity.Y);
		Debug.LogValue("radio new position ", newPosition);
		Debug.LogValue("radio position ", Position);
	}

	// Play the radio
	public void ChangeVolume()
	{
		// Get the distance from the player to the radio
		Vector2f playerPosition = Game.Player.Position;
		float distance = MathF.Sqrt(MathF.Pow((playerPosition.X - Position.X), 2) + MathF.Pow((playerPosition.Y - Position.Y), 2));

		// Change the volume depending on if the player is in range or not
		float volume = 0f;
		if (distance <= minDistance) volume = 100f;
		else if (distance <= maxDistance)
		{
			float distanceRange = maxDistance - minDistance;
			float distanceFactor = distance - minDistance;
			volume = 100f - ((distanceFactor / distanceRange) * 100);
		}

		// Set the radios volume
		radio.Volume = volume;
	}



	// Check for if the radio is colliding with a tile
	// TODO: Put this in the GameObject class
	private bool CollidingWithTile(Vector2f newPosition, Tile tile)
	{
		// Calculate the player bounds based on the new position
		FloatRect bounds = new FloatRect(newPosition, new Vector2f(Game.Map.TileSize, Game.Map.TileSize));

		// Check for collision
		return bounds.Intersects(tile.Bounds);
	}
}