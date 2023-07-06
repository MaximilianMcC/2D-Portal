using SFML.Graphics;
using SFML.System;
using SFML.Window;

class RayCast
{
	// TODO: Don't put this in its own file/class
	public static Tile ShootRayCast(Vector2f startPosition, Vector2f direction)
	{
		Vector2f rayPosition = startPosition;

		// Keep going until the ray hits something
		while (true)
		{
			rayPosition += direction;

			// Check for if the ray hits something
			foreach (Tile tile in Game.Map.Tiles)
			{
				if (tile.Bounds.Contains(rayPosition.X, rayPosition.Y)) return tile;
			}
		}
	}
}