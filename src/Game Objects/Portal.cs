using SFML.System;
using SFML.Graphics;

class Portal : GameObject
{
	protected Player player;

	// Update the portal
	public override void Update()
	{
		base.Update();
	}

	public override void Start()
	{
		player = Game.GameObjects.OfType<Player>().First();
	}
}

// Blue portal
class BluePortal : Portal
{
	public Portal OrangePortal { private get; set; }

	// Make a new blue portal
	public BluePortal(Vector2f position)
	{
		Position = position;
		Sprite = new Sprite(new Texture("./assets/sprites/missing.png"));
	}

	public override void Update()
	{
		// Check for if the player has collided with the portal
		if (Collision() == player)
		{
			// Send the player through the portal, and keep their velocity
			player.Position = new Vector2f(OrangePortal.Position.X - 32, OrangePortal.Position.Y);
		}

		base.Update();
	}
}

// Orange portal
class OrangePortal : Portal
{
	public Portal BluePortal { private get; set; }

	// Make a new orange portal
	public OrangePortal(Vector2f position)
	{
		Position = position;
		Sprite = new Sprite(new Texture("./assets/sprites/missing.png"));
	}

	public override void Update()
	{
		// Check for if the player has collided with the portal
		if (Collision() == player)
		{
			// Send the player through the portal, and keep their velocity
			player.Position = new Vector2f(BluePortal.Position.X + 32, BluePortal.Position.Y);
		}

		base.Update();
	}
}