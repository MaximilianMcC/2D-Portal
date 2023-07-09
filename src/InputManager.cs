using SFML.Window;

//! I'm not sure if this is the best way to do this, but it works
// TODO: Add support for multiple bindings for the same option. For example, arrow keys and WASD.
class InputManager
{
	public static Input Inputs = new Input();
	private static Dictionary<Mouse.Button, bool> previousMouseState = new Dictionary<Mouse.Button, bool>();
	private static Dictionary<Keyboard.Key, bool> previousKeyState = new Dictionary<Keyboard.Key, bool>();



	// Check for if a key is being held down
	public static bool KeyHeld(Keyboard.Key key)
	{
		return Keyboard.IsKeyPressed(key);
	}

	// Check for if a key has been pressed a single time
	public static bool KeyPressed(Keyboard.Key key)
	{
		// Check for if the key was pressed in this frame, and wasn't in the previous frame
        bool isKeyPressed = Keyboard.IsKeyPressed(key);
        bool isPressedOnce = isKeyPressed && (!previousKeyState.ContainsKey(key) || !previousKeyState[key]);
        
        // Update the previous state for the next frame
        previousKeyState[key] = isKeyPressed;
        return isPressedOnce;
	}

	// Check for if a mouse button is being held down
	public static bool MouseHeld(Mouse.Button button)
	{
		return Mouse.IsButtonPressed(button);
	}

	// Check for if a mouse button is clicked a single time
    public static bool MouseClicked(Mouse.Button button)
    {
		// Check for if the button was clicked in this frame, and wasn't in the previous frame
        bool isButtonPressed = Mouse.IsButtonPressed(button);
        bool isClickedOnce = isButtonPressed && (!previousMouseState.ContainsKey(button) || !previousMouseState[button]);
        
        // Update the previous state for the next frame
        previousMouseState[button] = isButtonPressed;
        return isClickedOnce;
    }

}

// TODO: Also include mouse and controller support
struct Input
{
	// Movement inputs
	public Keyboard.Key MoveUp;
	public Keyboard.Key MoveDown;
	public Keyboard.Key MoveLeft;
	public Keyboard.Key MoveRight;
	public Keyboard.Key jump;

	// Gameplay inputs
	public Keyboard.Key Interact;
	public Mouse.Button FireBlue;
	public Mouse.Button FireOrange;
	


	public Input()
	{
		// Movement inputs
		this.MoveUp = Keyboard.Key.Up;
		this.MoveDown = Keyboard.Key.Up;
		this.MoveLeft = Keyboard.Key.Left;
		this.MoveRight = Keyboard.Key.Right;
		this.jump = Keyboard.Key.Space;

		// Gameplay inputs
		this.Interact = Keyboard.Key.E;
		this.FireBlue = Mouse.Button.Left;
		this.FireOrange = Mouse.Button.Right;
	}
}