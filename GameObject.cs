using SFML.Graphics;

public interface GameObject
{
	public Sprite Sprite { get; set; }

	void Start();
	void Update();
	void Render();
}