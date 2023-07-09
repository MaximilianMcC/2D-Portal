using SFML.Graphics;
using SFML.System;
using SFML.Window;

class GameObject
{
	public Vector2f Position;
	protected Sprite sprite;


	/// <summary>
	/// Create a new game object instance, and add it to the game
	/// </summary>
	public GameObject()
	{
		// Add the current game object to the game
		Game.GameObjects.Add(this);
	}



	/// <summary>
	/// Runs a single time before the Update() method.
	/// </summary>
	public virtual void Start() {}



	/// <summary>
	/// Update game logic for this game object.
	/// Runs once every frame.
	/// </summary>
	public virtual void Update() {}



	/// <summary>
	/// Render the game object.
	/// Also sets the sprites position to the current position.
	/// </summary>
	public virtual void Render()
	{
		// Update the sprites position, then draw it
		sprite.Position = Position;
		Game.Window.Draw(sprite);
	}

}