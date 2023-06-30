using SFML.System;
using SFML.Graphics;

public class GameObject
{
	public Vector2f Position { get; set; }
	protected Sprite sprite;

	// Runs before the first frame is drawn
	public virtual void Start()
	{
		this.Position = new Vector2f(0, 0);
		this.sprite = new Sprite();
	}

	// Perform logic every frame
	public virtual void Update()
	{

	}

	// Draw the game object every frame
	public virtual void Render()
	{
		sprite.Position = Position;
		Game.Window.Draw(sprite);
	}

	// Get the bounds of the sprite
	public FloatRect GetBounds()
	{
		return sprite.GetGlobalBounds();
	}
}