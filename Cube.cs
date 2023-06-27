using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Cube : GameObject
{
	public Sprite Sprite { get; set; }
	public Vector2f Position { get; protected set; }
	protected float mass = 25f;

	public Cube(Vector2f spawnPoint)
	{
		this.Position = spawnPoint;

		// Add the cube to the list of game objects
		Game.GameObjects.Add(this);
	}

	public void Start()
	{

	}

	public void Update()
	{

	}

	public void Render()
	{
		Game.Window.Draw(Sprite);
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
		this.Sprite = new Sprite(new Texture("./assets/sprites/cube.png"));
		Sprite.Position = Position;
	}
}


// Companion cube
class CompanionCube : Cube
{
	public CompanionCube(Vector2f spawnPoint) : base(spawnPoint)
	{
		// Create the sprite
		this.Sprite = new Sprite(new Texture("./assets/sprites/companion-cube.png"));
		Sprite.Position = Position;
	}
}
