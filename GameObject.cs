using SFML.Graphics;
using SFML.System;
using SFML.Window;

class GameObject
{
	public Vector2f Position;
	protected Sprite sprite;


	/// <summary>
	/// <param>Create a new game object instance, and add it to the game</param>
	/// </summary>
	public GameObject()
	{
		// Add the current game object to the game
		Game.GameObjects.Add(this);
	}



	/// <summary>
	/// <param>Runs a single time before the Update() method.</param>
	/// </summary>
	public virtual void Start() {}



	/// <summary>
	/// <param>Update game logic for this game object.</param>
	/// <param>Runs once every frame.</param>
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