using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

class PortalGun
{
	private readonly float coolDownSeconds = 0.3f;
	private readonly Clock coolDownClock = new Clock();
	private bool onCoolDown = false;
	private float angle = 180f; // right (-1, 0)


	// Shoot a blue portal
	public void ShootPortal(PortalType portalType)
	{

		// Check for if there is a cool down
		if (onCoolDown == false)
		{
			Console.WriteLine("Shooting portal");

			// Check for what block the portal is going to shoot
			// TODO: Make the angle change based on the players cursor
			Vector2f direction = new Vector2f(MathF.Cos(angle), MathF.Sin(angle));
			Tile portalTarget = RayCast.ShootRayCast(Game.Player.Position, direction);
			portalTarget.ToggleHighlight();
			Console.WriteLine(portalTarget.Position);

			// Check for what portal they want to shoot
			if (portalType == PortalType.BLUE) new Sound(new SoundBuffer("./assets/sounds/fire-blue.wav")).Play();
			else if (portalType == PortalType.ORANGE) new Sound(new SoundBuffer("./assets/sounds/fire-orange.wav")).Play();

			// TODO: Check for where the portal is gonna shoot
			// TODO: Check for if the block is portalable (portal can go on it)




			// Reset the cool down for the next shot
			onCoolDown = true;
			coolDownClock.Restart();
		}
		else 
		{
			// Check for if the cool down is over
			if (coolDownClock.ElapsedTime.AsSeconds() >= coolDownSeconds) onCoolDown = false;
		}
	}
}


enum PortalType
{
	BLUE,
	ORANGE
}