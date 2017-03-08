/// <summary>
/// Settings of the game.
/// </summary>
public class GameSettings {
    /// <summary>
    /// The name of the game.
    /// </summary>
    public string gameName;
    /// <summary>
	/// Number of players.
	/// </summary>
	public int maxPlayerCount;

	/// <summary>
	/// Initializes a new instance of the <see cref="GameSettings"/> class.
	/// </summary>
	/// <param name="playerCount">Player count.</param>
	public GameSettings(string name, int playerCount) {
		this.maxPlayerCount = playerCount;
	}
	public GameSettings() {
        this.gameName = "Unnamed Game";
		this.maxPlayerCount = 10;
	}
}
