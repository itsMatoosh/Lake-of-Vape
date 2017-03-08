using UnityEngine;

/// <summary>
/// Manages the game.
/// Server-only.
/// </summary>
public class GameManager : MonoBehaviour {
	/// <summary>
	/// Currently running game.
	/// </summary>
	public static Game currentGame;
    /// <summary>
    /// Instance of the class.
    /// </summary>
    public static GameManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame(GameSettings settings) {
		//Creating the game instance.
		currentGame = new Game(settings);
	}
}
