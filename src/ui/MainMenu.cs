using SFML.Graphics;
using SFML.System;

class MainMenu
{
	private Text menuText;
	private Font font;


	// Create all of the things in the menu
	public MainMenu()
	{
		font = new Font("./assets/fonts/upheavtt.ttf");

		// Main menu text
		uint fontSize = (uint)(Game.Window.Size.X / 8);
		menuText = new Text("2D-PORTAL", font, fontSize);
		menuText.FillColor = new Color(0xff6a01ff);

		// Place the menu text in the top middle of the window
		menuText.Origin = new Vector2f((menuText.GetGlobalBounds().Width / 2), (menuText.GetGlobalBounds().Height / 2));
		menuText.Position = new Vector2f((Game.Window.Size.X / 2), 150);
	}

	public void Render()
	{
		// Draw the menu text
		Game.Window.Draw(menuText);
	}

}