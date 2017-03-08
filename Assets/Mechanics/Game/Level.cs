using System.Collections;
using System.Collections.Generic;

public class Level {
	/// <summary>
	/// The name of the level.
	/// </summary>
	public string name;
	/// <summary>
	/// The rounds played.
	/// </summary>
	public List<Round> roundsPlayed;

	/// <summary>
	/// Initializes a new instance of the <see cref="Level"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	public Level(string name) {
		this.name = name;
		this.roundsPlayed = new List<Round> ();
	}
}
