using DiscordRPC;
using DiscordRPC.Events;

class Discord
{
	private DiscordRpcClient client;
	private RichPresence presence;

	// Start the rich presence
	public void Start()
	{
		// Create the client
		client = new DiscordRpcClient("1133654009589272687");
		client.Initialize();

		// Make the initial rich presence
		presence = new RichPresence()
		{
			Assets = new Assets() { LargeImageKey = "icon-text" },
			Timestamps = new Timestamps() { Start = DateTime.UtcNow }
		};
		client.SetPresence(presence);
	}

	// Update the state
	public void UpdateState(State state)
	{
		if (state == State.DEBUGGING) presence.State = "Debugging";
		else if (state == State.PLAYING)
		{
			// Give a random bit of text
			Random random = new Random();
			string[] text = new[]
			{
				"Mastering the Aperture Science Handheld Portal Device",
				"Navigating the Enrichment Center",
				"Outsmarting GLaDOS",
				"Defying the Laws of Physics",
				"Retrieving the Companion Cube",
				"Evading Sentry Turrets",
				"Navigating Test Chambers",
				"Escaping the Test Facility",
				"Unleashing the Power of Portals",
				"Thinking with Portals",
				"Standing on the 1500 Megawatt Aperture Science Heavy Duty Super-Colliding Super Button"
				// Thanks, Clyde for the text🔥👍
			};

			presence.State = text[random.Next(0, text.Length)];
		}
		
		client.SetPresence(presence);
	}

	// Update the map/location
	public void UpdateDetails(string levelName)
	{
		presence.Details = "Level: " + levelName;
		client.SetPresence(presence);
	}



	// Kill the rpc
	// TODO: Run this when game stops
	public void Stop()
	{
		client.Dispose();
	}
}

public enum State
{
	PLAYING,
	AFK,
	DEBUGGING
}