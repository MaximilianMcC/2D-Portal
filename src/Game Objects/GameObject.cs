using SFML.Graphics;
using SFML.System;

class GameObject
{
	public Vector2f Position { get; set; }
	public Sprite Sprite { get; set; }




	// Create a new game object
	public GameObject()
	{
		// Add the game object to the game
		Game.GameObjects.Add(this);
	}




	// Start method. Called once when the game starts.
	public virtual void Start()
	{

	}

	// Update method. Called once every frame.
	public virtual void Update()
	{

	}

	// Render method. Called once every frame.
	public virtual void Render()
	{
		// Set the sprites position to the objects position, then render it
		Sprite.Position = Position;
		Game.Window.Draw(Sprite);
	}

}