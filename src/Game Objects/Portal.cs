using SFML.System;
using SFML.Graphics;

class Portal : GameObject
{
	protected Player player;
	// TODO: Don't put these in manually
	protected int width = 6;
	protected int height = 32;

	public override void Start()
	{
		player = Game.GameObjects.OfType<Player>().First();
	}

	protected Sprite SetSprite(Sprite sprite, Direction direction)
	{
		// Make the sprite have the correct rotation, and direction
		if (direction == Direction.LEFT || direction == Direction.RIGHT) sprite.Scale = new Vector2f((int)direction, 1);
		else sprite.Rotation = (float)direction;

		return sprite;
	}

	protected FloatRect SetBounds(Vector2f position)
	{
		return new FloatRect(position, new Vector2f(width, height));
	}
}

// Blue portal
class BluePortal : Portal
{
	public Portal OrangePortal { private get; set; }

	// Make a new blue portal
	public BluePortal(Vector2f position, Direction direction)
	{
		Position = position;

		// Make the sprite and set the bounds
		Sprite = SetSprite(new Sprite(new Texture("./assets/sprites/portal-blue.png")), direction);
		Bounds = SetBounds(position);
	}

	public override void Update()
	{
		// Check for if the player has collided with the portal
		if (Collision() == player)
		{
			// Send the player through the portal, and keep their velocity
			player.Position = new Vector2f(OrangePortal.Position.X - (width * width), OrangePortal.Position.Y);
		}
	}
}

// Orange portal
class OrangePortal : Portal
{
	public Portal BluePortal { private get; set; }

	// Make a new orange portal
	public OrangePortal(Vector2f position, Direction direction)
	{
		Position = position;

		// Make the sprite and set the bounds
		Sprite = SetSprite(new Sprite(new Texture("./assets/sprites/portal-orange.png")), direction);
		Bounds = SetBounds(position);
	}

	public override void Update()
	{
		// Check for if the player has collided with the portal
		if (Collision() == player)
		{
			// Send the player through the portal, and keep their velocity
			player.Position = new Vector2f(BluePortal.Position.X + width, BluePortal.Position.Y);
		}
	}
}