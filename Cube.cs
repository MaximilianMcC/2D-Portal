using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Cube : GameObject
{
	protected float mass = 25f;

	public Cube(Vector2f spawnPoint)
	{
		this.Position = spawnPoint;

		// Add the cube to the list of game objects
		Game.GameObjects.Add(this);
	}

}


public enum CubeType
{
	WEIGHTED,
	COMPANION
}













// Weighted cube
class WeightedCube : Cube
{
	public WeightedCube(Vector2f spawnPoint) : base(spawnPoint)
	{
		// Create the sprite
		this.sprite = new Sprite(new Texture("./assets/sprites/cube.png"));
		sprite.Position = Position;
	}
}


// Companion cube
class CompanionCube : Cube
{
	public CompanionCube(Vector2f spawnPoint) : base(spawnPoint)
	{
		// Create the sprite
		this.sprite = new Sprite(new Texture("./assets/sprites/companion-cube.png"));
		sprite.Position = Position;
	}
}
