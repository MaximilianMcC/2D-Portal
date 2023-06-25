using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Debug
{
	private static Font font = new Font("./assets/fonts/upheavtt.ttf");
	private static uint fontSize = 30;
	private static List<Text> chat = new List<Text>();
	private static float padding = 5;
	private static Color textColor = new Color(0x000000ff);

	// Log a string
	public static void Log(object message)
	{
		// Make the text
		Text text = new Text(message.ToString(), font, fontSize);
		text.FillColor = textColor;

		// Set its position
		Vector2f position = new Vector2f(padding, ((text.GetGlobalBounds().Height + padding) * chat.Count));
		text.Position = position;

		// Add it to the chat log thing
		chat.Add(text);
	}

	// Render all of the debug messages
	public static void RenderDebugMessages()
	{
		for (int i = 0; i < chat.Count; i++)
		{
			Game.Window.Draw(chat[i]);
		}

		// Clear the chat for the next messages
		chat.Clear();
	}

}