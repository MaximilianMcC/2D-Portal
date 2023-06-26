using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Debug
{
	private static Font font = new Font("./assets/fonts/upheavtt.ttf");
	private static uint fontSize = 30;
	private static List<Text[]> chat = new List<Text[]>();
	private static float padding = 10;
	private static Color textColor = new Color(0xffffffff);
	private static Color valueTextColor = new Color(0x00ff00ff);

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
		Text[] currentMessage = new Text[] { text };
		chat.Add(currentMessage);
	}


	// Log a value
	public static void LogValue(string title, object value)
	{
		// Make the text
		Text titleText = new Text(title, font, fontSize);
		Text valueText = new Text(value.ToString(), font, fontSize);
		titleText.FillColor = textColor;
		valueText.FillColor = valueTextColor;

		// Set its position
		Vector2f titleTextPosition = new Vector2f(padding, ((titleText.GetGlobalBounds().Height + padding) * chat.Count));
		titleText.Position = titleTextPosition;

		Vector2f valueTextPosition = new Vector2f((padding + titleText.GetGlobalBounds().Width + padding), ((valueText.GetGlobalBounds().Height + padding) * chat.Count));
		valueText.Position = valueTextPosition;

		// Add it to the chat log thing
		Text[] currentMessage = new Text[] { titleText, valueText };
		chat.Add(currentMessage);
	}


	// Render all of the debug messages
	public static void RenderDebugMessages()
	{
		float backgroundHeight = 40 * chat.Count;

		// Draw a background so that the text can be read more easily
		// TODO: Make the x dynamic
		RectangleShape background = new RectangleShape(new Vector2f(Game.Window.Size.X, backgroundHeight));
		background.FillColor = new Color(0x1e1e1ebb);
		Game.Window.Draw(background);

		// Draw the text
		for (int i = 0; i < chat.Count; i++)
		{
			for (int j = 0; j < chat[i].Length; j++)
			{
				Game.Window.Draw(chat[i][j]);
			}
		}

		// Clear the chat for the next messages
		chat.Clear();
	}

}