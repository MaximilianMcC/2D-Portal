using SFML.Audio;
using SFML.Graphics;
using SFML.System;

class Radio : GameObject
{
	Sound radio;
	float minDistance = 300f; // The distance where the radio sound is at 100% volume
	float maxDistance = 1000f; // The distance where the radio sound fades out to 0% volume
	
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

		//! debug
		Debug.LogValue("Distance to radio: ", distance);
		Debug.LogValue("Radio volume rn:   ", radio.Volume);
	}
}