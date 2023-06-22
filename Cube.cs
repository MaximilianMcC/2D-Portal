using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Cube : GameObject
{
	public CubeType Type { get; private set; }
	public Vector2f Position { get; private set;}

	private Sprite sprite;
	private float yVelocity;
	private float mass = 25f;

	public Cube(CubeType cubeType, Vector2f spawnPoint)
	{
		this.Type = cubeType;

		// Assign the texture depending on the type
		string texturePath = "./assets/sprites/missing.png";
		if (cubeType == CubeType.WEIGHTED) texturePath = "./assets/sprites/cube.png";
		else if (cubeType == CubeType.COMPANION) texturePath = "./assets/sprites/companion-cube.png";

		// Create the sprite
		this.sprite = new Sprite(new Texture(texturePath));
		this.Position = spawnPoint;
		sprite.Position = Position;

		// Add the cube to the list of game objects
		Game.GameObjects.Add(this);
	}

	public void Start()
	{

	}

	public void Update()
	{
		Vector2f newPosition = Position;

		// Apply gravity
		float gravityForce = Game.Map.Gravity * mass;
		yVelocity += gravityForce;
		newPosition.Y += yVelocity * Game.DeltaTime;

		// Update the cubes position
		Position = newPosition;
		sprite.Position = Position;
	}

	public void Render()
	{
		Game.Window.Draw(sprite);
	}

}


public enum CubeType
{
	WEIGHTED,
	COMPANION
}