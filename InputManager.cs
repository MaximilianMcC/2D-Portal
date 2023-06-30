using SFML.Window;

//! I'm not sure if this is the best way to do this, but it works
// TODO: Add support for multiple bindings for the same option. For example, arrow keys and WASD.
class InputManager
{
	public static Input Keys = new Input();


	// Check for if a key is being held down
	public static bool KeyHeld(Keyboard.Key key)
	{
		return Keyboard.IsKeyPressed(key);
	}

	// TODO: Check for if a key has been pressed a single time

}

// TODO: Also include mouse and controller support
struct Input
{
	// Movement keys
	public Keyboard.Key MoveUp;
	public Keyboard.Key MoveDown;
	public Keyboard.Key MoveLeft;
	public Keyboard.Key MoveRight;
	public Keyboard.Key jump;

	// Gameplay keys
	public Keyboard.Key Interact;


	public Input()
	{
		// Movement keys
		this.MoveUp = Keyboard.Key.Up;
		this.MoveDown = Keyboard.Key.Up;
		this.MoveLeft = Keyboard.Key.Left;
		this.MoveRight = Keyboard.Key.Right;
		this.jump = Keyboard.Key.Space;

		// Gameplay keys
		this.Interact = Keyboard.Key.E;
	}
}