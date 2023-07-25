using SFML.System;
using SFML.Graphics;

class Portal : GameObject
{
	protected Player player;
	// TODO: Don't put these in manually
	protected int width = 1;
	protected int height = 32;
	protected Direction direction;

	public override void Start()
	{
		player = Game.GameObjects.OfType<Player>().First();
	}

	// Set the portals sprite
	protected Sprite SetSprite(Sprite sprite)
	{
		// Make the sprite have the correct rotation, and direction
		if (direction == Direction.LEFT || direction == Direction.RIGHT) sprite.Scale = new Vector2f((int)direction, 1);
		else sprite.Rotation = (float)direction;

		return sprite;
	}

	// Set the portal collision bounds
	protected FloatRect SetBounds()
	{
		Vector2f boundsPosition = Position;

		// Adjust the position
		if (direction == Direction.LEFT) boundsPosition.X -= width;
		// else if (direction == Direction.RIGHT) boundsPosition.X += width;

		return new FloatRect(boundsPosition, new Vector2f(width, height));
	}

	// Check for portal collision, then teleport the player
	protected void PortalCollision(Portal otherPortal)
	{
		// Check for if a player hits a portal
		if (Collision() != player) return;

		// Get the players new position using the distances
		float exitPositionX = otherPortal.Position.X;
		float exitPositionY = otherPortal.Position.Y;

		// Adjust for the direction
		if (otherPortal.direction == Direction.RIGHT) exitPositionX -= player.Bounds.Width;

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
		// Assign variables
		Position = position;
		this.direction = direction;

		// Make the sprite and set the bounds
		Sprite = SetSprite(new Sprite(new Texture("./assets/sprites/portal-blue.png")));
		Bounds = SetBounds();
	}

	public override void Update()
	{
		// Check for portal collision, then teleport the player
		PortalCollision(OrangePortal);
		Debug.LogValue("blue bounds:", Bounds);
	}
}

// Orange portal
class OrangePortal : Portal
{
	public Portal BluePortal { private get; set; }

	// Make a new orange portal
	public OrangePortal(Vector2f position, Direction direction)
	{
		// Assign variables
		Position = position;
		this.direction = direction;

		// Make the sprite and set the bounds
		Sprite = SetSprite(new Sprite(new Texture("./assets/sprites/portal-orange.png")));
		Bounds = SetBounds();
	}

	public override void Update()
	{
		// Check for portal collision, then teleport the player
		PortalCollision(BluePortal);
		Debug.LogValue("orange bounds:", Bounds);
	}
}