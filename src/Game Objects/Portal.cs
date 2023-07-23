using SFML.System;
using SFML.Graphics;

class Portal : GameObject
{
	protected Player player;
	// TODO: Don't put these in manually
	protected int width = 6; //! values lower than 6 break it idk
	protected int height = 32;

	public override void Start()
	{
		player = Game.GameObjects.OfType<Player>().First();
	}

	// Set the portals sprite
	protected Sprite SetSprite(Sprite sprite, Direction direction)
	{
		// Make the sprite have the correct rotation, and direction
		if (direction == Direction.LEFT || direction == Direction.RIGHT) sprite.Scale = new Vector2f((int)direction, 1);
		else sprite.Rotation = (float)direction;

		return sprite;
	}

	// Set the portal collision bounds
	protected FloatRect SetBounds(Vector2f position)
	{
		return new FloatRect(position, new Vector2f(width, height));
	}

	// Check for portal collision, then teleport the player
	protected void PortalCollision(Portal otherPortal)
	{
		// Check for if a player hits a portal
		if (Collision() != player) return;

		// Get the difference between both portals
		float differenceX = otherPortal.Position.X - Position.X;
		float differenceY = otherPortal.Position.Y - Position.Y;

		// Get the players new position using the distances
		float exitPositionX = player.Position.X + differenceX;
		float exitPositionY = player.Position.Y + differenceY;

		// Switch the players direction if needed
		if (player.direction == Direction.LEFT) exitPositionX -= width;
		else exitPositionX += width;

		// Move the player to the exit position, making them go to the other portal
		player.Position = new Vector2f(exitPositionX, exitPositionY);
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
		// Check for portal collision, then teleport the player
		PortalCollision(OrangePortal);
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
		// Check for portal collision, then teleport the player
		PortalCollision(BluePortal);
	}
}