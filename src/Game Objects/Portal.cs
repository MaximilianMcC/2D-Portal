using SFML.System;
using SFML.Graphics;

class Portal : GameObject
{
	protected Player player;
	protected int width = 1;
	protected int height = 32;
	protected Direction direction;

	public override void Start()
	{
		// Get the player
		player = Game.GameObjects.OfType<Player>().First();
	}

	// Set the portals sprite
	protected void SetSprite(Sprite sprite)
	{
		// Make the sprite have the correct rotation, and direction
		if (direction == Direction.LEFT || direction == Direction.RIGHT) sprite.Scale = new Vector2f((int)direction, 1);
		else sprite.Rotation = (float)direction;

		// Assign the sprite
		Sprite = sprite;
	}

	// Set the portal collision bounds
	protected void SetBounds()
	{
		Vector2f boundsPosition = Position;

		// Adjust the positions depending on the direction
		if (direction == Direction.LEFT) boundsPosition.X -= width;
		else if (direction == Direction.UP) boundsPosition.Y += height;
		else if (direction == Direction.DOWN) boundsPosition.Y -= height;

		Bounds = new FloatRect(boundsPosition, new Vector2f(width, height));
	}

	// Check for portal collision, then teleport the player
	protected void PortalCollision(Portal otherPortal)
	{
		// Check for if a player hits a portal
		if (Collision() != player) return;

		// Get the players new position using the distances
		float exitPositionX = otherPortal.Position.X;
		float exitPositionY = otherPortal.Position.Y;

		// Adjust for the direction on the X
		if (otherPortal.direction == Direction.RIGHT) exitPositionX -= player.Bounds.Width;

		// Adjust for the direction on the Y
		if (otherPortal.direction == Direction.UP)
		{
			exitPositionY -= player.Bounds.Height;
			exitPositionX -= player.Bounds.Width;
		}

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
		SetSprite(new Sprite(new Texture("./assets/sprites/portal-blue.png")));
		SetBounds();
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
		// Assign variables
		Position = position;
		this.direction = direction;

		// Make the sprite and set the bounds
		SetSprite(new Sprite(new Texture("./assets/sprites/portal-orange.png")));
		SetBounds();
	}

	public override void Update()
	{
		// Check for portal collision, then teleport the player
		PortalCollision(BluePortal);
	}
}